using Microsoft.Extensions.Logging;
using System;

namespace RPedretti.RazorComponents.Sensors.Geolocation
{
    public class GeolocationSensorService
    {
        #region Properties

        private ILogger<GeolocationSensorService> Logger { get; }

        #endregion Properties

        #region Events

        public event EventHandler<PositionError> OnPositionError;

        public event EventHandler<Position> OnPositionUpdate;

        #endregion Events

        #region Constructors

        public GeolocationSensorService(ILogger<GeolocationSensorService> logger)
        {
            Logger = logger;
        }

        #endregion Constructors

        #region Methods

        public void NotifyPositionError(PositionError error)
        {
            OnPositionError?.Invoke(this, error);
        }

        public void NotifyPositionResponse(Position position)
        {
            OnPositionUpdate?.Invoke(this, position);
        }

        #endregion Methods
    }
}
