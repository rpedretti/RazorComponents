using System;

namespace RPedretti.RazorComponents.Sensors.Geolocation
{
    public class Position
    {
        #region Properties

        public Coordinates Coords { get; set; }
        public DateTime Timestamp { get; set; }

        #endregion Properties
    }
}
