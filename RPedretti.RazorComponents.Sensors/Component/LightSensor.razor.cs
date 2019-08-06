using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Shared.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sensors.Component
{
    public class LightSensorBase : ComponentBase, IDisposable
    {
        private JSReferenceFactory JSReferenceFactory;
        private DotNetObjectRef<LightSensorBase> thisRef;
        private bool init;

        [Parameter] protected bool Render { get; set; }
        [Parameter] protected int? PollTime { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private IComponentContext ComponentContext { get; set; }
        [Inject] private ILogger<LightSensor> Logger { get; set; }

        [Parameter] protected EventCallback<int> OnValue { get; set; }
        [Parameter] protected EventCallback<string> OnError { get; set; }

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

        [JSInvokable]
        public async Task NotifyAmbientLightError(string error)
        {
            await OnError.InvokeAsync(error);
        }

        [JSInvokable]
        public async Task NotifyReading(int illuminance)
        {
            await OnValue.InvokeAsync(illuminance);
        }

        public void Dispose()
        {
            try
            {
                if (thisRef != null)
                {
                    if (ComponentContext.IsConnected)
                    {
                        JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.lightSensor.stopSensor", thisRef).ContinueWith(_ => {
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
    }
}
