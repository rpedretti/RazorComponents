using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class GeolocatonPoint
    {
        #region Properties

        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }

        #endregion Properties

        #region Constructors

        public GeolocatonPoint()
        {
        }

        public GeolocatonPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion Constructors

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is GeolocatonPoint point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(X, Y);
        }

        #endregion Methods
    }
}
