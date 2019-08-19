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

        [Parameter] public bool Active { get; set; }
        [Parameter] public bool Centered { get; set; }
        [Parameter] public SpinnerSize Size { get; set; } = SpinnerSize.SMALL;

        #endregion Properties
    }
}
