#nullable enable

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Shared.Components
{
    public abstract class BaseInteropComponent<T> : BaseComponent where T : class, new()
    {
        #region Properties

        private T? _interop { get; set; }
        protected DotNetObjectReference<T>? JsInteropRef { get; set; }

        #endregion Properties

        #region Methods

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (JsInteropRef == null)
            {
                JsInteropRef = DotNetObjectReference.Create(_interop!);
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        public virtual void Dispose()
        {
            JsInteropRef?.Dispose();
        }

        public void SetInterop(T interop)
        {
            _interop = interop;
        }

        #endregion Methods
    }
}

#nullable restore
