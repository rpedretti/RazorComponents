using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Shared.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.SuggestBox
{
    public class SuggestBoxBase<T> : SuggestBoxBaseJSInterop
    {
        #region Fields

        private readonly DebounceDispatcher _queryDispatcher = new DebounceDispatcher();
        private string _a11ylabel;
        private bool _init = true;
        private bool _loading;
        private string _originalQuery;
        private bool _shouldRender;
        protected readonly string directions = "Keyboard users, use up and down arrows to review and enter to select. Touch device users, explore by touch or with swipe gestures.";
        protected List<SuggestionItem<T>> _suggestionItems = new List<SuggestionItem<T>>();
        protected List<T> _suggestions;
        protected bool AnnounceA11Y = false;
        protected ElementReference input;
        protected string internalQuery;

        #endregion Fields

        #region Properties

        [Inject] private ILogger<SuggestBoxBase<T>> Logger { get; set; }

        protected string A11yLabel
        {
            get => _a11ylabel;
            set => SetParameter(ref _a11ylabel, value, () => AnnounceA11Y = true);
        }

        protected bool HasFocus { get; set; }

        protected string ListId { get; set; }

        protected bool OpenSuggestion { get; set; }

        [Parameter] public int DebounceTime { get; set; } = 500;
        [Parameter] public string Description { get; set; }

        [Parameter]
        public bool LoadingSuggestion
        {
            get => _loading;
            set
            {
                SetParameter(ref _loading, value, () =>
                {
                    UpdateLoadingA11yLabel(value);
                    _shouldRender = true;
                });
            }
        }

        [Parameter] public int MaxSuggestions { get; set; }

        [Parameter]
        public string Query
        {
            get => internalQuery;
            set
            {
                if (SetParameter(ref internalQuery, value))
                {
                    Logger.LogDebug($"setting query: {value}");
                    _queryDispatcher.Debounce(DebounceTime, (v) => InvokeAsync(() => QueryChanged.InvokeAsync(v as string)), value);
                    _originalQuery = internalQuery;
                }
            }
        }

        [Parameter] public EventCallback<string> QueryChanged { get; set; }

        [Parameter]
        public List<T> Suggestions
        {
            get => _suggestions;
            set
            {
                SetParameter(ref _suggestions, value, () =>
                {
                    OpenSuggestion = value?.Count > 0;
                    _suggestions = value;
                    if (_suggestions != null)
                    {
                        Logger.LogDebug($"suggestions: {string.Join(",", value)}");
                        _suggestionItems = _suggestions.Select(s => new SuggestionItem<T>
                        {
                            Selected = false,
                            Value = s
                        }).ToList();
                    }

                    AnnounceA11Y = true;
                    A11yLabel = _suggestionItems?.Count > 0 ? $"{ _suggestionItems.Count } results. { directions }" : "no results";
                    _shouldRender = true;
                });
            }
        }

        [Parameter] public EventCallback<T> SuggestionSelected { get; set; }

        [Parameter] public RenderFragment<SuggestionItem<T>> SuggestionTemplate { get; set; }

        #endregion Properties

        #region Constructors

        public SuggestBoxBase()
        {
            ClearSelectionEvent += (s, e) => ClearSuggestSelection();
        }

        #endregion Constructors

        #region Methods

        private void UpdateLoadingA11yLabel(bool loading)
        {
            if (loading)
            {
                AnnounceA11Y = true;
                A11yLabel = "Loading";
            }
        }

        protected async Task HandleKeyDown(KeyboardEventArgs args)
        {
            if (_suggestionItems.Any())
            {
                SuggestionItem<T> newSelected;
                switch (args.Key)
                {
                    case "Enter":
                        if (!OpenSuggestion && _suggestionItems.Any())
                        {
                            OpenSuggestion = true;
                        }
                        else
                        {
                            var selected = _suggestionItems.FirstOrDefault(i => i.Selected);
                            if (selected != null)
                            {
                                await InternalSuggestionSelected(selected);
                            }
                        }

                        _shouldRender = true;
                        break;

                    case "ArrowUp":
                        if (OpenSuggestion)
                        {
                            var currentSelectedUp = _suggestionItems.FirstOrDefault(i => i.Selected);
                            if (currentSelectedUp != null)
                            {
                                currentSelectedUp.Selected = false;
                                if (_suggestionItems.First() == currentSelectedUp)
                                {
                                    newSelected = _suggestionItems.Last();
                                }
                                else
                                {
                                    newSelected = _suggestionItems[_suggestionItems.IndexOf(currentSelectedUp) - 1];
                                }
                            }
                            else
                            {
                                newSelected = _suggestionItems.Last();
                            }

                            internalQuery = newSelected.Value.ToString();
                            newSelected.Selected = true;
                        }
                        _shouldRender = true;
                        break;

                    case "ArrowDown":
                        if (OpenSuggestion)
                        {
                            var currentSelectedDown = _suggestionItems.FirstOrDefault(i => i.Selected);
                            if (currentSelectedDown != null)
                            {
                                currentSelectedDown.Selected = false;
                                if (_suggestionItems.Last() == currentSelectedDown)
                                {
                                    newSelected = _suggestionItems.First();
                                }
                                else
                                {
                                    var currIndex = _suggestionItems.IndexOf(currentSelectedDown);
                                    newSelected = _suggestionItems[currIndex + 1];
                                }
                            }
                            else
                            {
                                newSelected = _suggestionItems.First();
                            }

                            newSelected.Selected = true;
                            internalQuery = newSelected.Value.ToString();
                            _shouldRender = true;
                        }

                        _shouldRender = true;
                        break;

                    case "Tab":
                    case "Escape":
                        ClearSelection();
                        _shouldRender = true;
                        break;
                }
            }
        }

        protected async Task InternalSuggestionSelected(SuggestionItem<T> item)
        {
            internalQuery = item.Value.ToString();
            _originalQuery = internalQuery;
            _suggestionItems.Clear();
            OpenSuggestion = false;
            await SuggestionSelected.InvokeAsync(item.Value);
            AnnounceA11Y = true;
            A11yLabel = null;
            await SetSuggestionAsync();
            _shouldRender = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (_init)
            {
                _init = false;
                await InitAsync();
            }
            AnnounceA11Y = false;
        }

        protected override void OnInitialized()
        {
            SuggestBoxId = $"suggestbox-{Guid.NewGuid()}";
        }

        protected override bool ShouldRender() => _shouldRender;

        public void ClearSuggestSelection()
        {
            _suggestionItems.ForEach(s => s.Selected = false);
            internalQuery = _originalQuery;
            OpenSuggestion = false;
            Logger.LogDebug($"Clear selection: {internalQuery}");
            StateHasChanged();
        }

        #endregion Methods
    }

    public sealed class SuggestionItem<T>
    {
        #region Properties

        public bool Selected { get; set; }
        public T Value { get; set; }

        #endregion Properties
    }
}
