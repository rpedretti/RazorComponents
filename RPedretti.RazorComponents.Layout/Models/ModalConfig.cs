using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Layout.Models
{
    public sealed class ModalConfig
    {
        #region Properties

        public bool CloseOnOverlayClick { get; set; }
        public RenderFragment Content { get; set; }
        public bool LockScroll { get; set; }

        #endregion Properties
    }
}
