using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.Layout.Models
{
    public sealed class ModalConfig
    {
        public bool CloseOnOverlayClick { get; set; }
        public bool LockScroll { get; set; }
        public RenderFragment Content { get; set; }
    }
}
