using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Sensors.AmbientLight;
using RPedretti.RazorComponents.Shared.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sensors.Component
{
    public abstract class LightSensorBase : ComponentBase, IDisposable
    {
        #region Fields

        private bool init;
        private JSReferenceFactory JSReferenceFactory;
        private DotNetObjectRef<LightSensorBase> thisRef;

        #endregion Fields

        #region Properties

        [Inject] private AmbientLightSensorService ambientLightSensorService { get; set; }

        [Inject] private IComponentContext ComponentContext { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private ILogger<LightSensor> Logger { get; set; }
        [Parameter] public EventCallback<string> OnError { get; set; }
        [Parameter] public EventCallback<int> OnValue { get; set; }
        [Parameter] public double? PollTime { get; set; }
        [Parameter] public bool Render { get; set; }

        #endregion Properties

        #region Methods

        protected override async Task OnAfterRenderAsync()
        {
            if (ComponentContext.IsConnected && !init)
            {
                init = true;
                JSReferenceFactory = new JSReferenceFactory(JSRuntime);
                thisRef = JSReferenceFactory.CreateDotNetObjectRef(this);
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.lightSensor.initSensor", thisRef, PollTime);
            }
            await base.OnAfterRenderAsync();
        }

        public void Dispose()
        {
            try
            {
                if (thisRef != null)
                {
                    if (ComponentContext.IsConnected)
                    {
                        JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.lightSensor.stopSensor", thisRef).ContinueWith(_ =>
                        {
                            JSReferenceFactory.DisposeDotNetObjectRef(thisRef);
                        });
                    }
                }
                init = false;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error disposing");
            }
        }

        [JSInvokable]
        public async Task NotifyAmbientLightError(string error)
        {
            ambientLightSensorService.NotifyAmbientLightError(error);
            await OnError.InvokeAsync(error);
        }

        [JSInvokable]
        public async Task NotifyReading(int illuminance)
        {
            ambientLightSensorService.NotifyAmbientLightReading(illuminance);
            await OnValue.InvokeAsync(illuminance);
        }

        #endregion Methods
    }
}
