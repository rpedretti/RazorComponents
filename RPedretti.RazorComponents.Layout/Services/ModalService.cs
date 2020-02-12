using RPedretti.RazorComponents.Layout.Models;
using System;

namespace RPedretti.RazorComponents.Layout.Services
{
    public class ModalService : IModalService
    {
        #region Properties

        public bool IsOpen { get; private set; }

        #endregion Properties

        #region Events

        public event EventHandler<ShowModalEventArgs> ShowChanged;

        #endregion Events

        #region Methods

        public void Hide()
        {
            IsOpen = false;
            ShowChanged?.Invoke(this, new ShowModalEventArgs { Show = false });
        }

        public void Show(ModalConfig config = null)
        {
            var modalConfig = config ?? null;
            IsOpen = true;
            ShowChanged?.Invoke(this, new ShowModalEventArgs
            {
                Show = true,
                CloseOnOverlayClick = modalConfig.CloseOnOverlayClick,
                Content = modalConfig.Content
            });
        }

        #endregion Methods
    }
}
