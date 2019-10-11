using System;
using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Layout.Models;

namespace RPedretti.RazorComponents.Layout.Services
{
    public interface IModalService
    {
        event EventHandler<ShowModalEventArgs> ShowChanged;
        bool IsOpen { get; }
        void Hide();
        void Show(ModalConfig modalArgs = null);
    }
}