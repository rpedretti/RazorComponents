using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Shared.Components;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Input.SuggestBox
{
    public class SuggestBoxBaseJSInterop : BaseInteropComponent<SuggestBoxBaseJSInterop>
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        public string SuggestBoxId { get; set; }

        #region Events

        public event EventHandler ClearSelectionEvent;

        #endregion Events

        #region Methods

        public SuggestBoxBaseJSInterop()
        {
            SetInterop(this);
        }

        public async ValueTask SetSuggestionAsync()
        {
            await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.suggestbox.setSuggestion", SuggestBoxId);
        }
        public async ValueTask InitAsync()
        {
            await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.suggestbox.initSuggestBox", JsInteropRef, SuggestBoxId);
        }

        [JSInvokable]
        public void ClearSelection()
        {
            ClearSelectionEvent?.Invoke(this, EventArgs.Empty);
        }

        public override void Dispose()
        {
            if (JsInteropRef != null)
            {
                JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.suggestbox.unregisterSuggestBox", SuggestBoxId)
                    .AsTask().ContinueWith(t => base.Dispose());
            }
        }

        #endregion Methods
    }
}
