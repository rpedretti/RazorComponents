using Microsoft.JSInterop;

namespace RPedretti.RazorComponents.Shared.JSInterop
{
    public class JSReferenceFactory
    {
        #region Fields

        private static object CreateDotNetObjectRefSyncObj = new object();

        #endregion Fields

        #region Properties

        private IJSRuntime Js { get; set; }

        #endregion Properties

        #region Constructors

        public JSReferenceFactory(IJSRuntime jsRuntime)
        {
            Js = jsRuntime;
        }

        #endregion Constructors

        #region Methods

        public DotNetObjectRef<T> CreateDotNetObjectRef<T>(T value) where T : class
        {
            lock (CreateDotNetObjectRefSyncObj)
            {
                JSRuntime.SetCurrentJSRuntime(Js);
                return DotNetObjectRef.Create(value);
            }
        }

        public void DisposeDotNetObjectRef<T>(DotNetObjectRef<T> value) where T : class
        {
            if (value != null)
            {
                lock (CreateDotNetObjectRefSyncObj)
                {
                    JSRuntime.SetCurrentJSRuntime(Js);
                    value.Dispose();
                }
            }
        }

        #endregion Methods
    }
}
