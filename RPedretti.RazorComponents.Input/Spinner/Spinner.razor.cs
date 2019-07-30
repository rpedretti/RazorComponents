using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Input.Spinner
{
    public enum SpinnerSize
    {
        EXTRA_SMALL,
        SMALL,
        MEDIUM,
        LARGE
    }

    public class SpinnerBase : ComponentBase
    {
        #region Properties

        [Parameter] protected bool Active { get; set; }

        [Parameter] protected bool Centered { get; set; }
        [Parameter] protected SpinnerSize Size { get; set; } = SpinnerSize.SMALL;

        #endregion Properties
    }
}
