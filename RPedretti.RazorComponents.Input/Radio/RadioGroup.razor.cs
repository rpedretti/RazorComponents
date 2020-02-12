using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.Radio
{
    public partial class RadioGroup
    {
        #region Properties

        [Parameter] public IEnumerable<RadioButton> Buttons { get; set; } = new List<RadioButton>();

        [Parameter] public bool CanDeselect { get; set; }

        [Parameter] public RadioOrientation Orientation { get; set; } = RadioOrientation.VERTICAL;

        [Parameter] public RadioButton? Selected { get; set; }

        [Parameter] public EventCallback<RadioButton> SelectedChanged { get; set; }

        #endregion Properties

        #region Methods

        protected async Task Click(RadioButton button)
        {
            var t = button.Disabled ? Task.CompletedTask : SelectButton(button);
            await t;
        }

        protected async Task KeyDown(KeyboardEventArgs args, RadioButton button)
        {
            var t = button.Disabled ? Task.CompletedTask : HandleKeyPress(args, () => SelectButton(button));
            await t;
        }

        protected async Task SelectButton(RadioButton button)
        {
            if (Selected == button && CanDeselect)
            {
                Selected = null;
            }
            else if (Selected != button)
            {
                Selected = button;
                await SelectedChanged.InvokeAsync(button);
            }
        }

        #endregion Methods
    }
}
