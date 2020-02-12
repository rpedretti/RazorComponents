using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Sensors.AmbientLight;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sensors.Geolocation
{
    public partial class Geolocation : IDisposable
    {
        #region Fields

        private bool? _internalWatching;
        private bool init;
        private DotNetObjectReference<Geolocation> thisRef;
        private int? watchId;

        #endregion Fields

        #region Properties

        [Inject] private GeolocationSensorService GeolocationSensorService { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private ILogger<LightSensor> Logger { get; set; }

        [Parameter] public EventCallback<PositionError> OnError { get; set; }
        [Parameter] public EventCallback<Position> OnValue { get; set; }
        [Parameter] public bool Render { get; set; }
        [Parameter] public bool Watching { get; set; }

        #endregion Properties

        #region Methods

        private async Task StartWatch()
        {
            try
            {
                if (thisRef == null)
                {
                    thisRef = DotNetObjectReference.Create(this);
                }
                watchId = await JSRuntime.InvokeAsync<int>("rpedrettiBlazorSensors.geolocation.watchPosition", thisRef);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error starting watch");
            }
        }

        private async Task StopWatch()
        {
            try
            {
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.geolocation.stopWatchPosition", watchId);
                watchId = null;
                init = false;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error stopping watch");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!init)
            {
                init = true;
                thisRef = DotNetObjectReference.Create(this);
            }
            if (Watching != _internalWatching)
            {
                if (Watching)
                {
                    await StartWatch();
                }
                else
                {
                    await StopWatch();
                }
                _internalWatching = Watching;
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            if (watchId.HasValue)
            {
                StopWatch().ContinueWith(_ => thisRef.Dispose());
            }
            else
            {
                thisRef.Dispose();
            }
        }

        [JSInvokable]
        public async Task WatchPositionError(PositionError error)
        {
            GeolocationSensorService.NotifyPositionError(error);
            await OnError.InvokeAsync(error);
        }

        [JSInvokable]
        public async Task WatchPositionResponse(Position position)
        {
            GeolocationSensorService.NotifyPositionResponse(position);
            await OnValue.InvokeAsync(position);
        }

        #endregion Methods
    }
}
