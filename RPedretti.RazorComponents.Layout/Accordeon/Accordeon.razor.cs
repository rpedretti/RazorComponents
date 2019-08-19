﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Shared.Components;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Layout.Accordeon
{
    public class AccordeonBase : BaseComponent
    {
        #region Fields

        private bool _expanded;
        protected bool showChildren = true;

        #endregion Fields

        #region Properties

        [Inject]
        private ILogger<AccordeonBase> Logger { get; set; }

        [Parameter] public bool CenterTitle { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Expanded
        {
            get => _expanded;
            set
            {
                Logger.LogDebug($"expanded: {value}");
                SetParameter(ref _expanded, value, () =>
                {
                    var delay = !value ? 600 : 0;
                    new Timer(_ => InvokeAsync(async () =>
                    {
                        await ExpandedChanged.InvokeAsync(_expanded);
                        showChildren = value;
                        StateHasChanged();
                    }), null, delay, Timeout.Infinite);
                });
            }
        }

        [Parameter] public EventCallback<bool> ExpandedChanged { get; set; }
        [Parameter] public string Title { get; set; }

        #endregion Properties

        #region Methods

        protected async Task ToggleExpanded()
        {
            Expanded = !Expanded;
            await Task.CompletedTask;
        }

        #endregion Methods
    }
}
