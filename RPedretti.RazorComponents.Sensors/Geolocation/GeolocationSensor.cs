using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Shared.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sensors.Geolocation
{
    public class GeolocationSensor : IDisposable
    {
        #region Fields

        private object _lock = new Object();
        private bool init;
        private JSReferenceFactory JSReferenceFactory;
        private DotNetObjectRef<GeolocationSensor> thisRef;

        private int? watchId;
        private IJSRuntime JSRuntime;

        public ILogger<GeolocationSensor> Logger { get; }

        public GeolocationSensor(ILogger<GeolocationSensor> logger)
        {
            Logger = logger;
        }

        #endregion Fields

        #region Events

        private event EventHandler<PositionError> _onPositionError;

        private event EventHandler<Position> _onPositionUpdate;

        #endregion Events

        #region Methods

        public void Init(IJSRuntime jSRuntime)
        {
            JSRuntime ??= jSRuntime;
        }

        private void AssureInit()
        {
            lock (_lock)
            {
                if (!init)
                {
                    init = true;
                    StartWatch();
                }
            }
        }

        private void AssureStop()
        {
            lock (_lock)
            {
                if (_onPositionUpdate == null && _onPositionError == null)
                {
                    StopWatch();
                }
            }
        }

        private void StartWatch()
        {
            try
            {
                if (thisRef == null)
                {
                    JSReferenceFactory = new JSReferenceFactory(JSRuntime);
                    thisRef = JSReferenceFactory.CreateDotNetObjectRef(this);
                }
                JSRuntime.InvokeAsync<int>("rpedrettiBlazorSensors.geolocation.watchPosition", thisRef).ContinueWith(id =>
                {
                    watchId = id.Result;
                });
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error starting watch");
            }
        }

        private void StopWatch()
        {
            try
            {
                JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.geolocation.stopWatchPosition", watchId);
                watchId = null;
                init = false;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error stopping watch");
            }
        }

        #endregion Methods

        public event EventHandler<PositionError> OnPositionError
        {
            add
            {
                AssureInit();
                _onPositionError += value;
            }
            remove
            {
                _onPositionError -= value;
                AssureStop();
            }
        }

        public event EventHandler<Position> OnPositionUpdate
        {
            add
            {
                AssureInit();
                _onPositionUpdate += value;
            }
            remove
            {
                _onPositionUpdate -= value;
                AssureStop();
            }
        }

        public Task RequestPositionResponse(Position position)
        {
            return Task.CompletedTask;
        }

        public Task WatchPositionError(PositionError error)
        {
            _onPositionError?.Invoke(this, error);
            return Task.CompletedTask;
        }

        public Task WatchPositionResponse(Position position)
        {
            _onPositionUpdate?.Invoke(this, position);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            JSReferenceFactory.DisposeDotNetObjectRef(thisRef);
        }
    }
}
