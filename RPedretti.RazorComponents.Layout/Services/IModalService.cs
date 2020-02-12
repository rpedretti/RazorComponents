using RPedretti.RazorComponents.Layout.Models;
using System;

namespace RPedretti.RazorComponents.Layout.Services
{
    public interface IModalService
    {
        #region Properties

        bool IsOpen { get; }

        #endregion Properties

        #region Events

        event EventHandler<ShowModalEventArgs> ShowChanged;

        #endregion Events

        #region Methods

        void Hide();

        void Show(ModalConfig modalArgs = null);

        #endregion Methods
    }
}
