using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap.Modules
{
    public abstract class BaseBingMapModule : IBingMapModule
    {

        public BaseBingMapModule(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        protected IJSRuntime JSRuntime { get; set; }

        #region Methods

        protected async Task InitModuleAsync(string mapId, string moduleName, string initFuncName = null, object initFuncParams = null)
        {
            await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.loadModule",
                mapId, moduleName, initFuncName, initFuncParams ?? new { });
        }

        public abstract Task InitAsync(string mapId);

        #endregion Methods
    }
}
