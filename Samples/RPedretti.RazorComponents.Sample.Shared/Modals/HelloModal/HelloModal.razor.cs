using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Modals.HelloModal
{
    public partial class HelloModal
    {
        #region Properties

        [Parameter] public EventCallback OnClose { get; set; }

        #endregion Properties

        #region Methods

        protected async Task EmitClose()
        {
            await OnClose.InvokeAsync(null);
        }

        #endregion Methods
    }
}
