using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Modules;
using RPedretti.RazorComponents.BingMap.Modules.Directions;
using RPedretti.RazorComponents.Shared.Components;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.BingMap.Pages.Directions
{
    public class DirectionsPageBase : BaseComponent
    {
        #region Fields

        private BingMapDirectionsModule _directionsModule;

        protected string BingMapId = $"bing-map-directions";
        protected ObservableCollection<IBingMapModule> Modules = new ObservableCollection<IBingMapModule>();

        #endregion Fields

        #region Properties

        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] protected ILogger<DirectionsPageBase> Logger { get; set; }

        protected BingMapConfig MapsConfig { get; set; } = new BingMapConfig
        {
            MapTypeId = BingMapTypes.Road,
            SupportedMapTypes = new string[] {
                BingMapTypes.Aerial,
                BingMapTypes.GrayScale,
                BingMapTypes.Road,
                BingMapTypes.BirdsEyes
            },
            EnableHighDpi = true,
            Zoom = 12,
            ShowTrafficButton = true
        };

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();

        #endregion Properties

        #region Methods

        private void DirectionsUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("path updated");
        }

        protected Task MapLoaded()
        {
            Logger.LogDebug("MapLoaded");
            return Task.CompletedTask;
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Logger.LogDebug("Map rendered");
                _directionsModule = new BingMapDirectionsModule(JSRuntime)
                {
                    InputPanelId = "inputPannel",
                    ItineraryPanelId = "itineraryPanel"
                };

                _directionsModule.DirectionsUpdated += DirectionsUpdated;
                Modules.Add(_directionsModule);
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        #endregion Methods
    }
}
