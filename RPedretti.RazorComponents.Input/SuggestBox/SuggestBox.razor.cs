using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Shared.Components;
using RPedretti.RazorComponents.Shared.JSInterop;
using RPedretti.RazorComponents.Shared.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.SuggestBox
{
    public class SuggestBoxBase<T> : BaseComponent, IDisposable
    {
        #region Fields
        private string _a11ylabel;
        private bool _loading;
        private bool _shouldRender;
        private bool init = true;
        private string originalQuery;
        private DebounceDispatcher queryDispatcher = new DebounceDispatcher();
        private SuggestBoxBaseJSInterop interop = new SuggestBoxBaseJSInterop();

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private IComponentContext ComponentContext { get; set; }
        [Inject]
        private ILogger<SuggestBoxBase<T>> Logger { get; set; }

        protected ElementRef input;

        #endregion Fields

        #region Methods

        private void UpdateLoadingA11yLabel(bool loading)
        {
            if (loading)
            {
                AnnounceA11Y = true;
                A11yLabel = "Loading";
            }
        }

        #endregion Methods

        protected readonly string directions = "Keyboard users, use up and down arrows to review and enter to select. Touch device users, explore by touch or with swipe gestures.";
        protected List<SuggestionItem<T>> _suggestionItems = new List<SuggestionItem<T>>();
        protected List<T> _suggestions;
        protected bool AnnounceA11Y = false;

        private DotNetObjectRef<SuggestBoxBaseJSInterop> ObjRef { get; set; }

        protected string internalQuery;

        protected string A11yLabel
        {
            get => _a11ylabel;
            set => SetParameter(ref _a11ylabel, value, () => AnnounceA11Y = true);
        }

        [Parameter] protected string Description { get; set; }
        protected bool HasFocus { get; set; }
        protected string ListId { get; set; }

        [Parameter] protected RenderFragment<SuggestionItem<T>> SuggestionTemplate { get; set; }

        [Parameter]
        protected bool LoadingSuggestion
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
        [Parameter] protected int MaxSuggestions { get; set; }
        protected bool OpenSuggestion { get; set; }

        [Parameter]
        protected string Query
        {
            get => internalQuery;
            set
            {
                if (SetParameter(ref internalQuery, value))
                {
                    Logger.LogDebug($"setting query: {value}");
                    queryDispatcher.Debounce(1000, (v) => InvokeAsync(() => QueryChanged.InvokeAsync(v as string)), value);
                    originalQuery = internalQuery;
                }
            }
        }

        [Parameter] protected EventCallback<string> QueryChanged { get; set; }

        [Parameter]
        protected List<T> Suggestions
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
                        Logger.LogDebug($"suggestions: {string.Join(',', value)}");
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

        [Parameter] protected EventCallback<T> SuggestionSelected { get; set; }

        public SuggestBoxBase()
        {
            this.interop.ClearSelectionEvent += (s, e) => ClearSelection();
        }

        protected async Task HandleKeyDown(UIKeyboardEventArgs args)
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
            _suggestionItems.Clear();
            OpenSuggestion = false;
            await SuggestionSelected.InvokeAsync(item.Value);
            AnnounceA11Y = true;
            A11yLabel = null;
            await JSRuntime.InvokeAsync<int>("rpedrettiBlazorComponents.suggestbox.focusById", SuggestBoxId);
            _shouldRender = true;
        }

        protected override async Task OnAfterRenderAsync()
        {

            if (ComponentContext.IsConnected)
            {
                if (init)
                {
                    JSReferenceFactory = new JSReferenceFactory(JSRuntime);
                    init = false;
                    ObjRef = JSReferenceFactory.CreateDotNetObjectRef(interop);
                    await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.suggestbox.initSuggestBox", ObjRef, SuggestBoxId);
                }
                AnnounceA11Y = false;
                await base.OnAfterRenderAsync();
            }
        }

        protected override void OnInit()
        {
            SuggestBoxId = $"suggestbox-{Guid.NewGuid()}";
        }

        protected override bool ShouldRender()
        {
            return _shouldRender;
        }

        internal string SuggestBoxId { get; set; }
        private JSReferenceFactory JSReferenceFactory { get; set; }

        public void ClearSelection()
        {
            _suggestionItems.ForEach(s => s.Selected = false);
            internalQuery = originalQuery;
            OpenSuggestion = false;
            Logger.LogDebug($"Clear selection: {internalQuery}");
            StateHasChanged();
        }

        public async void Dispose()
        {
            if (ComponentContext.IsConnected)
            {
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.suggestbox.unregisterSuggestBox", SuggestBoxId);
                JSReferenceFactory.DisposeDotNetObjectRef(ObjRef);
            }
        }
    }

    public sealed class SuggestionItem<T>
    {
        #region Properties

        public bool Selected { get; set; }
        public T Value { get; set; }

        #endregion Properties
    }
}
