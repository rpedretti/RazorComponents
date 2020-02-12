using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Layout.Services;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Modal
{
    public partial class ModalPage
    {
        #region Properties

        private RenderFragment? _modalContent { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Inject] private IModalService _modalService { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        #endregion Properties

        #region Methods

        private void Close()
        {
            _modalService.Hide();
        }

        public void OpenModal(bool overlayClose)
        {
            _modalService.Show(new Layout.Models.ModalConfig
            {
                CloseOnOverlayClick = overlayClose,
                Content = _modalContent
            });
        }

        #endregion Methods
    }
}
