using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.Shared.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sensors.AmbientLight
{
    public class AmbientLightSensor : IDisposable
    {
        private JSReferenceFactory JSReferenceFactory;
        private DotNetObjectRef<AmbientLightSensor> thisRef;
        private bool init = false;

        #region Events

        private event EventHandler<string> _onError;

        #endregion Events

        #region Methods

        public AmbientLightSensor(ILogger<AmbientLightSensor> logger)
        {
            Logger = logger;
        }

        public void Init(IJSRuntime jSRuntime)
        {
            try
            {
                if (!init)
                {
                    JSRuntime = jSRuntime;
                    JSReferenceFactory = new JSReferenceFactory(JSRuntime);
                    thisRef = JSReferenceFactory.CreateDotNetObjectRef(this);
                    JSRuntime.InvokeAsync<object>("rpedrettiBlazorSensors.lightsensor.initSensor", thisRef);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error initizlizing");
            }
        }

        #endregion Methods

        #region Properties

        public string Error { get; private set; }
        private IJSRuntime JSRuntime { get; set; }
        public ILogger<AmbientLightSensor> Logger { get; }

        #endregion Properties

        public event EventHandler<int> _onReading;

        public event EventHandler<string> OnError
        {
            add
            {
                _onError += value;
                if (Error != null)
                {
                    value.Invoke(this, Error);
                }
            }
            remove
            {
                _onError -= value;
            }
        }

        public event EventHandler<int> OnReading
        {
            add
            {
                _onReading += value;
                if (Error != null)
                {
                    _onError?.Invoke(this, Error);
                }
            }
            remove
            {
                _onReading -= value;
            }
        }

        public void Dispose()
        {
            try
            {
                if (thisRef != null)
                {
                    JSReferenceFactory.DisposeDotNetObjectRef(thisRef);
                }
                init = false;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error disposing");
            }
        }

        public Task NotifyAmbientLightError(string error)
        {
            Error = error;
            _onError?.Invoke(this, error);
            return Task.CompletedTask;
        }

        public Task NotifyAmbientLightReading(int illuminance)
        {
            _onReading?.Invoke(this, illuminance);
            return Task.CompletedTask;
        }
    }
}
