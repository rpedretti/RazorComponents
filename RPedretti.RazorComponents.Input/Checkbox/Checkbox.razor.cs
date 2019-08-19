using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Shared.Components;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.Checkbox
{
    public class CheckboxBase : BaseAccessibleComponent
    {
        #region Properties

        protected string CheckboxId { get; set; } = Guid.NewGuid().ToString().Replace("-", "", StringComparison.InvariantCulture);

        [Parameter] public bool Checked { get; set; }

        [Parameter] public EventCallback<bool> CheckedChanged { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public bool Inline { get; set; }

        [Parameter] public string Label { get; set; }

        [Parameter] public CheckboxSize Size { get; set; } = CheckboxSize.REGULAR;

        #endregion Properties

        #region Methods

        protected async Task KeyDown(UIKeyboardEventArgs args)
        {
            await (Disabled ? Task.CompletedTask : HandleKeyPress(args, ToggleCheck));
        }

        protected async Task ToggleCheck()
        {
            if (!Disabled)
            {
                Checked = !Checked;
                await CheckedChanged.InvokeAsync(Checked);
            }
        }

        #endregion Methods
    }
}
