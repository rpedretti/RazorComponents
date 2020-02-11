using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class LocationRectangle
    {
        #region Properties

        [JsonPropertyName("center")]
        public Geocoordinate Center { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is LocationRectangle rectangle &&
                   EqualityComparer<Geocoordinate>.Default.Equals(Center, rectangle.Center) &&
                   Height == rectangle.Height &&
                   Width == rectangle.Width;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Center, Height, Width);
        }

        #endregion Methods
    }
}
