using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.Sensors.Geolocation
{
    public class Coordinates
    {
        #region Properties

        [JsonPropertyName("accuracy")]
        public double Accuracy { get; set; }

        [JsonPropertyName("altitude")]
        public double? Altitude { get; set; }

        [JsonPropertyName("altitudeAccuracy")]
        public double? AltitudeAccuracy { get; set; }

        [JsonPropertyName("heading")]
        public double? Heading { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("speed")]
        public double? Speed { get; set; }

        #endregion Properties
    }
}
