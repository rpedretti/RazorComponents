using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Sensors.Geolocation;
using RPedretti.RazorComponents.Shared.Components;
using RPedretti.RazorComponents.Shared.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sensors.Component
{
    public class GeolocationBase : BaseComponent, IDisposable
    {
        private JSReferenceFactory JSReferenceFactory;
        private DotNetObjectRef<GeolocationBase> thisRef;
        private bool init;
        private bool? _internalWatching;
        private int? watchId;

        [Parameter]
        protected bool Render { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Inject]
        private IComponentContext ComponentContext { get; set; }
        [Inject]
        private ILogger<LightSensor> Logger { get; set; }
        [Parameter] protected EventCallback<Position> OnValue { get; set; }
        [Parameter] protected EventCallback<PositionError> OnError { get; set; }

        [Parameter]
        protected bool Watching { get; set; }

        protected override async Task OnAfterRenderAsync()
        {
            if (ComponentContext.IsConnected)
            {
                if (!init)
                {
                    init = true;
                    JSReferenceFactory = new JSReferenceFactory(JSRuntime);
                    thisRef = JSReferenceFactory.CreateDotNetObjectRef(this);
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
            }

            await base.OnAfterRenderAsync();
        }

        private async Task StartWatch()
        {
            try
            {
                if (thisRef == null)
                {
                    JSReferenceFactory = new JSReferenceFactory(JSRuntime);
                    thisRef = JSReferenceFactory.CreateDotNetObjectRef(this);
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

        [JSInvokable]
        public async Task WatchPositionError(PositionError error)
        {
            await OnError.InvokeAsync(error);
        }

        [JSInvokable]
        public async Task WatchPositionResponse(Position position)
        {
            await OnValue.InvokeAsync(position);
        }

        public void Dispose()
        {
            if (watchId.HasValue)
            {
                StopWatch().ContinueWith(_ => JSReferenceFactory.DisposeDotNetObjectRef(thisRef));
            }
            else
            {
                JSReferenceFactory.DisposeDotNetObjectRef(thisRef);
            }
        }
    }
}
