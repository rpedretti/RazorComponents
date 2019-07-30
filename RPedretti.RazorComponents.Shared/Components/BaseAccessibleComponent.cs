using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Shared.Components
{
    public abstract class BaseAccessibleComponent : BaseComponent
    {
        #region Properties

        [Parameter] protected string A11yLabel { get; set; }
        [Parameter] protected int? A11yPosInSet { get; set; }
        [Parameter] protected string A11yRole { get; set; }
        [Parameter] protected int? A11ySetSize { get; set; }

        #endregion Properties
    }
}
