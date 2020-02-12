using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Layout.Services;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Shared
{
    public partial class MainLayout
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Inject] private ILogger<MainLayout> _logger { get; set; }
        [Inject] private IModalService _modalService { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        protected RenderFragment? ModalContent { get; set; }

        public bool Checked { get; set; }
        public int Light { get; set; }
        public string? LightError { get; set; }

        protected Task OnError(string error)
        {
            LightError = error;
            _logger.LogError(error);
            return Task.CompletedTask;
        }

        protected void OnReading(int reading)
        {
            Light = reading;
        }

        protected void ShowModal()
        {
            _modalService.Show(new Layout.Models.ModalConfig { CloseOnOverlayClick = false });
        }

        protected void Close()
        {
            _logger.LogInformation("closing");
            _modalService.Hide();
        }
    }
}
