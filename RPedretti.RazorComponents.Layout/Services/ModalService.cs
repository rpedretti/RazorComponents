using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Layout.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.Layout.Services
{
    public class ModalService : IModalService
    {
        private readonly IJSRuntime _jSRuntime;

        public event EventHandler<ShowModalEventArgs> ShowChanged;
        public bool IsOpen { get; private set; }

        public ModalService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public void Show(ModalConfig config = null)
        {
            var modalConfig = config ?? new ModalConfig();
            IsOpen = true;
            ShowChanged?.Invoke(this, new ShowModalEventArgs {
                Show = true, 
                CloseOnOverlayClick = modalConfig.CloseOnOverlayClick,
                Content = modalConfig.Content
            });

            if (config.LockScroll)
            {
                _jSRuntime.InvokeVoidAsync("rpedrettiBlazorComponents.modal.setScroll", false);
            }
        }

        public void Hide()
        {
            IsOpen = false;
            ShowChanged?.Invoke(this, new ShowModalEventArgs { Show = false });
            _jSRuntime.InvokeVoidAsync("rpedrettiBlazorComponents.modal.setScroll", true);
        }
    }
}
