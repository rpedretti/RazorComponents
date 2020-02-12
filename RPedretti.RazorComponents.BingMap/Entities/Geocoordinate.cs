using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class Geocoordinate
    {
        #region Properties

        [JsonPropertyName("altitude")]
        public double Altitude { get; set; }

        [JsonPropertyName("altitudeReference")]
        public double AltitudeReference { get; set; } = -1;

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is Geocoordinate geocoordinate &&
                   Latitude == geocoordinate.Latitude &&
                   Longitude == geocoordinate.Longitude &&
                   Altitude == geocoordinate.Altitude &&
                   AltitudeReference == geocoordinate.AltitudeReference;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Latitude, Longitude, Altitude, AltitudeReference);
        }

        #endregion Methods
    }
}
