using System;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.Sensors.Geolocation
{
    public class Position
    {
        #region Properties

        [JsonPropertyName("coords")]
        public Coordinates Coords { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        #endregion Properties
    }
}
