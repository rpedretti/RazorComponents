﻿using System.Collections.Generic;
using System.Drawing;

namespace RPedretti.RazorComponents.BingMap.Entities.Polygon
{
    public class BingMapPolygonOptions
    {
        public string Cursor { get; set; }
        public bool? Generalizable { get; set; }
        public Color FillColor { get; set; }
        public Color StrokeColor { get; set; }
        public int[] StrokeDashArray { get; set; }
        public int? StrokeThickness { get; set; }
        public bool? Visible { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BingMapPolygonOptions options &&
                   Cursor == options.Cursor &&
                   EqualityComparer<bool?>.Default.Equals(Generalizable, options.Generalizable) &&
                   EqualityComparer<Color>.Default.Equals(FillColor, options.FillColor) &&
                   EqualityComparer<Color>.Default.Equals(StrokeColor, options.StrokeColor) &&
                   EqualityComparer<int[]>.Default.Equals(StrokeDashArray, options.StrokeDashArray) &&
                   EqualityComparer<int?>.Default.Equals(StrokeThickness, options.StrokeThickness) &&
                   EqualityComparer<bool?>.Default.Equals(Visible, options.Visible);
        }

        public override int GetHashCode()
        {
            var hashCode = 1232602982;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Cursor);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(Generalizable);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(FillColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(StrokeColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(StrokeDashArray);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(StrokeThickness);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(Visible);
            return hashCode;
        }
    }
}