using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Input.ProgressBar
{
    public class ProgressBarBase : ComponentBase
    {
        #region Properties

        [Parameter] protected bool Active { get; set; }

        #endregion Properties
    }
}
