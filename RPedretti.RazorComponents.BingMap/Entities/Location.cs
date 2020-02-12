using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class Location
    {
        #region Properties

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        #endregion Properties

        #region Constructors

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion Constructors
    }
}
