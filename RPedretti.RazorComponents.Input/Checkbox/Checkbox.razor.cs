using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Shared.Components;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.Checkbox
{
    public class CheckboxBase : BaseAccessibleComponent
    {
        #region Fields

        protected string CheckboxId = Guid.NewGuid().ToString().Replace("-", "");

        #endregion Fields

        #region Properties

        [Parameter] protected bool Checked { get; set; }
        [Parameter] protected EventCallback<bool> CheckedChanged { get; set; }
        [Parameter] protected bool Disabled { get; set; }
        [Parameter] protected bool Inline { get; set; }
        [Parameter] protected string Label { get; set; }
        [Parameter] protected CheckboxSize Size { get; set; } = CheckboxSize.REGULAR;

        #endregion Properties

        #region Methods

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
