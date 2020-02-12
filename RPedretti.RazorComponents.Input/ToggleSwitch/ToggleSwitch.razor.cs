using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.ToggleSwitch
{
    public enum SwitchSize
    {
        SMALL,
        MEDIUM,
        LARGE
    }

    public partial class ToggleSwitch
    {
        #region Properties

        [Parameter] public bool Checked { get; set; }
        [Parameter] public EventCallback<bool> CheckedChanged { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool Fill { get; set; }
        [Parameter] public bool Inline { get; set; }
        [Parameter] public string? Label { get; set; }
        [Parameter] public bool Round { get; set; }
        [Parameter] public SwitchSize Size { get; set; } = SwitchSize.MEDIUM;

        #endregion Properties

        #region Methods

        protected async Task HandleChanged(ChangeEventArgs a)
        {
            Checked = (bool)a.Value;
            await CheckedChanged.InvokeAsync(Checked);
        }

        protected async Task KeyDown(KeyboardEventArgs args)
        {
            await (!Disabled ? HandleKeyPress(args, ToggleChecked) : Task.CompletedTask);
        }

        protected async Task ToggleChecked()
        {
            Checked = !Checked;
            await CheckedChanged.InvokeAsync(Checked);
        }

        #endregion Methods
    }
}
