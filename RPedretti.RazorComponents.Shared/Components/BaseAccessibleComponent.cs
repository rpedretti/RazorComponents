using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Shared.Components
{
    public abstract class BaseAccessibleComponent : BaseComponent
    {
        #region Properties

        [Parameter] public string A11yLabel { get; set; }
        [Parameter] public int? A11yPosInSet { get; set; }
        [Parameter] public string A11yRole { get; set; }
        [Parameter] public int? A11ySetSize { get; set; }

        #endregion Properties
    }
}
