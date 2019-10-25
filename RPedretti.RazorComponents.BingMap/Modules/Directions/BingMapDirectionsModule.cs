using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap.Modules.Directions
{
    public class BingMapDirectionsModule : BaseBingMapModule, IDisposable
    {
        #region Fields

        private const string ModuleId = "Microsoft.Maps.Directions";
        private DotNetObjectReference<BingMapDirectionsModule> thisRef;

        public BingMapDirectionsModule(IJSRuntime jSRuntime): base(jSRuntime)
        {
        }

        #endregion Fields

        #region Properties

        private string InitFunctionName =>
            "rpedrettiBlazorComponents.bingMap.map.modules.directions.init";

        public string InputPanelId { get; set; }

        public string ItineraryPanelId { get; set; }

        #endregion Properties

        #region Events

        public event EventHandler DirectionsUpdated;

        #endregion Events

        #region Methods

        [JSInvokable]
        public Task DirectionsUpdatedAsync()
        {
            DirectionsUpdated?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            thisRef?.Dispose();
        }

        public override async Task InitAsync(string mapId)
        {
            thisRef = DotNetObjectReference.Create(this);
            var param = new { InputPanelId, ItineraryPanelId, ModuleRef = thisRef };
            await InitModuleAsync(mapId, ModuleId, InitFunctionName, param);
        }

        #endregion Methods
    }
}
