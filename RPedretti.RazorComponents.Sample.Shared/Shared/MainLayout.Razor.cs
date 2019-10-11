using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Layout.Services;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject] ILogger<MainLayout> Logger { get; set; }
        [Inject] protected IModalService ModalService { get; set; }

        protected RenderFragment ModalContent { get; set; }

        public bool Checked { get; set; }
        public int Light { get; set; }
        public string LightError { get; set; }

        protected Task OnError(string error)
        {
            LightError = error;
            Logger.LogError(error);
            return Task.CompletedTask;
        }

        protected void OnReading(int reading)
        {
            Light = reading;
        }

        protected void ShowModal()
        {
            ModalService.Show(new Layout.Models.ModalConfig { CloseOnOverlayClick = false });
        }

        protected void Close()
        {
            Logger.LogInformation("closing");
            ModalService.Hide();
        }
    }
}
