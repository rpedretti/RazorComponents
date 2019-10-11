using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Layout.Models
{
    public sealed class ShowModalEventArgs
    {
        public bool Show { get; set; }
        public bool LockScroll { get; set; } = true;
        public bool CloseOnOverlayClick { get; set; }
        public RenderFragment Content { get; set; }
    }
}
