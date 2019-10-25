﻿using System.Collections.Generic;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class LocationRectangle
    {
        #region Properties

        public Geocoordinate Center { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public override bool Equals(object obj)
        {
            return obj is LocationRectangle rectangle &&
                   EqualityComparer<Geocoordinate>.Default.Equals(Center, rectangle.Center) &&
                   Height == rectangle.Height &&
                   Width == rectangle.Width;
        }

        public override int GetHashCode()
        {
            var hashCode = 2049175893;
            hashCode = hashCode * -1521134295 + EqualityComparer<Geocoordinate>.Default.GetHashCode(Center);
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            return hashCode;
        }

        #endregion Properties
    }
}
