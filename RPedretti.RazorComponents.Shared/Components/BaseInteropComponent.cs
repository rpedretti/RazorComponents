#nullable enable

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Shared.Components
{
    public abstract class BaseInteropComponent<T> : BaseComponent where T : class, new()
    {
        #region Properties

        private T? Interop { get; set; }
        protected DotNetObjectReference<T>? JsInteropRef { get; set; }

        #endregion Properties

        #region Methods

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (JsInteropRef == null)
            {
                JsInteropRef = DotNetObjectReference.Create(Interop!);
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        public virtual void Dispose()
        {
            JsInteropRef?.Dispose();
        }

        public void SetInterop(T interop)
        {
            Interop = interop;
        }

        #endregion Methods
    }
}

#nullable restore
