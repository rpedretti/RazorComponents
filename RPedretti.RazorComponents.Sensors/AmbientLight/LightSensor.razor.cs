using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sensors.AmbientLight
{
    public partial class LightSensor : IDisposable
    {
        #region Fields

        private bool _init;
        private DotNetObjectReference<LightSensor> _thisRef;

        #endregion Fields

        #region Properties

        [Inject] private AmbientLightSensorService AmbientLightSensorService { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private ILogger<LightSensor> Logger { get; set; }
        [Parameter] public EventCallback<string> OnError { get; set; }
        [Parameter] public EventCallback<int> OnValue { get; set; }
        [Parameter] public double? PollTime { get; set; }
        [Parameter] public bool Render { get; set; }

        #endregion Properties

        #region Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!_init)
            {
                _init = true;
                _thisRef = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.lightSensor.initSensor", _thisRef, PollTime);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            try
            {
                if (_thisRef != null)
                {
                    JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.lightSensor.stopSensor", _thisRef).AsTask()
                        .ContinueWith(t => _thisRef.Dispose());
                }
                _init = false;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error disposing");
            }
        }

        [JSInvokable]
        public async Task NotifyAmbientLightError(string error)
        {
            AmbientLightSensorService.NotifyAmbientLightError(error);
            await OnError.InvokeAsync(error);
        }

        [JSInvokable]
        public async Task NotifyReading(int illuminance)
        {
            AmbientLightSensorService.NotifyAmbientLightReading(illuminance);
            await OnValue.InvokeAsync(illuminance);
        }

        #endregion Methods
    }
}
