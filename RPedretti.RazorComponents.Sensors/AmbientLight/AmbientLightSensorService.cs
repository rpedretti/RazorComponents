using Microsoft.Extensions.Logging;
using System;

namespace RPedretti.RazorComponents.Sensors.AmbientLight
{
    public class AmbientLightSensorService
    {
        #region Properties

        public string Error { get; private set; }

        public ILogger<AmbientLightSensorService> Logger { get; }

        #endregion Properties

        #region Events

        private event EventHandler<string> _onError;

        private event EventHandler<int> _onReading;

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

        #endregion Events

        #region Constructors

        public AmbientLightSensorService(ILogger<AmbientLightSensorService> logger)
        {
            Logger = logger;
        }

        #endregion Constructors

        #region Methods

        internal void NotifyAmbientLightError(string error)
        {
            Error = error;
            _onError?.Invoke(this, error);
        }

        internal void NotifyAmbientLightReading(int illuminance)
        {
            _onReading?.Invoke(this, illuminance);
        }

        #endregion Methods
    }
}
