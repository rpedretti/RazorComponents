using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.Polyline
{
    public class BingMapPolylineOptions
    {
        #region Properties

        [JsonPropertyName("cursor")]
        public string Cursor { get; set; }

        [JsonPropertyName("generalizable")]
        public bool? Generalizable { get; set; }

        [JsonPropertyName("strokeColor")]
        public Color StrokeColor { get; set; }

        [JsonPropertyName("strokeDashArray")]
        public int[] StrokeDashArray { get; set; }

        [JsonPropertyName("strokeThickness")]
        public int? StrokeThickness { get; set; }

        [JsonPropertyName("visible")]
        public bool? Visible { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is BingMapPolylineOptions options &&
                   Cursor == options.Cursor &&
                   EqualityComparer<bool?>.Default.Equals(Generalizable, options.Generalizable) &&
                   EqualityComparer<Color>.Default.Equals(StrokeColor, options.StrokeColor) &&
                   EqualityComparer<int[]>.Default.Equals(StrokeDashArray, options.StrokeDashArray) &&
                   EqualityComparer<int?>.Default.Equals(StrokeThickness, options.StrokeThickness) &&
                   EqualityComparer<bool?>.Default.Equals(Visible, options.Visible);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Cursor, Generalizable, StrokeColor, StrokeDashArray, StrokeThickness, Visible);
        }

        #endregion Methods
    }
}
