using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Layout.Models;
using RPedretti.RazorComponents.Layout.Services;
using System;

namespace RPedretti.RazorComponents.Layout.Modal
{
    public partial class Modal : IDisposable
    {
        #region Fields

        protected readonly string Id = $"modal-{Guid.NewGuid().ToString().Replace("-", "")}";

        #endregion Fields

        #region Properties

        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        protected bool CloseOnOnverlayClick { get; private set; }
        protected RenderFragment Content { get; private set; }
        protected bool LockScroll { get; private set; }
        protected bool Show { get; private set; }

        #endregion Properties

        #region Methods

        private void ModalService_ShowChanged(object sender, ShowModalEventArgs args)
        {
            Show = args.Show;
            CloseOnOnverlayClick = args.CloseOnOverlayClick;
            Content = args.Content;
            LockScroll = args.LockScroll;
            JSRuntime.InvokeVoidAsync("rpedrettiBlazorComponents.modal.updateView", Show, args.LockScroll, Id);
            StateHasChanged();
        }

        protected void Hide()
        {
            if (CloseOnOnverlayClick) ModalService.Hide();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (!firstRender)
            {
                Show = ModalService.IsOpen;
            }
            base.OnAfterRender(firstRender);
        }

        protected override void OnInitialized()
        {
            ModalService.ShowChanged += ModalService_ShowChanged;
            base.OnInitialized();
        }

        public void Dispose()
        {
            ModalService.ShowChanged -= ModalService_ShowChanged;
        }

        #endregion Methods
    }
}
