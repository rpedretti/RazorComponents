using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Layout.Models;
using RPedretti.RazorComponents.Layout.Services;
using RPedretti.RazorComponents.Shared.Components;
using System;

namespace RPedretti.RazorComponents.Layout.Modal
{
    public class ModalBase : BaseAccessibleComponent, IDisposable
    {
        protected RenderFragment Content { get; private set; }
        protected bool LockScroll { get; private set; }
        protected bool Show { get; private set; }
        protected bool CloseOnOnverlayClick { get; private set; }
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected readonly string Id = $"modal-{Guid.NewGuid().ToString().Replace("-", "")}";

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
            {
                Show = ModalService.IsOpen;
            }
            base.OnAfterRender(firstRender);
        }

        protected void Hide()
        {
            if (CloseOnOnverlayClick) ModalService.Hide();
        }

        protected override void OnInitialized()
        {
            ModalService.ShowChanged += ModalService_ShowChanged;
            base.OnInitialized();
        }

        private void ModalService_ShowChanged(object sender, ShowModalEventArgs args)
        {
            Show = args.Show;
            CloseOnOnverlayClick = args.CloseOnOverlayClick;
            Content = args.Content;
            LockScroll = args.LockScroll;
            JSRuntime.InvokeVoidAsync("rpedrettiBlazorComponents.modal.updateView", Show, args.LockScroll, Id);
            StateHasChanged();
        }

        public void Dispose()
        {
            ModalService.ShowChanged -= ModalService_ShowChanged;
        }
    }
}
