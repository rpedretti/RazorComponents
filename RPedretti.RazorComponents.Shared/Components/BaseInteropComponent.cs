using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Shared.Components
{
    public abstract class BaseInteropComponent<T> : BaseComponent where T : class, new()
    {
        private T Interop { get; set; }
        protected DotNetObjectReference<T> JsInteropRef { get; set; }

        public void SetInterop(T interop)
        {
            Interop = interop;
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (JsInteropRef == null)
            {
                JsInteropRef = DotNetObjectReference.Create(Interop);
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        public virtual void Dispose()
        {
            JsInteropRef?.Dispose();
        }
    }
}
