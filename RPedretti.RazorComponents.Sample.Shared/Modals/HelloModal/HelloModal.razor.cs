using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Shared.Components;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Modals.HelloModal
{
    public class HelloModalBase : BaseAccessibleComponent
    {
        [Parameter] public EventCallback OnClose { get; set; }

        protected async Task EmitClose()
        {
            await OnClose.InvokeAsync(null);
        }
    }
}
