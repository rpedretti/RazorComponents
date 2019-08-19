using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Input.ProgressBar
{
    public class ProgressBarBase : ComponentBase
    {
        #region Properties

        [Parameter] public bool Active { get; set; }

        #endregion Properties
    }
}
