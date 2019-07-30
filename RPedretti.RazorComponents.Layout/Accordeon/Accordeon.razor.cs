using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using RPedretti.RazorComponents.Shared.Components;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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

        [Parameter] protected bool CenterTitle { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        [Parameter]
        protected bool Expanded
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

        [Parameter] protected EventCallback<bool> ExpandedChanged { get; set; }
        [Parameter] protected string Title { get; set; }

        protected async Task ToggleExpanded()
        {
            Expanded = !Expanded;
            await Task.CompletedTask;
        }

        #endregion Properties
    }
}
