using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Shared.Components;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.ToggleSwitch
{
    public enum SwitchSize
    {
        SMALL,
        MEDIUM,
        LARGE
    }

    public class ToggleSwitchBase : BaseAccessibleComponent
    {
        #region Properties

        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected EventCallback<bool> CheckedChanged { get; set; }
        [Parameter] protected bool Disabled { get; set; }
        [Parameter] protected bool Fill { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected string Label { get; set; }
        [Parameter] protected bool Round { get; set; }
        [Parameter] protected SwitchSize Size { get; set; } = SwitchSize.MEDIUM;

        #endregion Properties

        #region Methods

        protected async Task HandleChanged(UIChangeEventArgs a)
        {
            Checked = (bool)a.Value;
            await CheckedChanged.InvokeAsync(Checked);
        }

        protected async Task ToggleChecked()
        {
            Checked = !Checked;
            await CheckedChanged.InvokeAsync(Checked);
        }

        #endregion Methods
    }
}
