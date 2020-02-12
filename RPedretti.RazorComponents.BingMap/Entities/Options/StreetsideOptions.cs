using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class StreetsideOptions
    {
        #region Properties

        [JsonPropertyName("disablePanoramaNavigation")]
        public bool? DisablePanoramaNavigation { get; set; }

        [JsonPropertyName("locationToLookAt")]
        public Geocoordinate LocationToLookAt { get; set; }

        [JsonPropertyName("overviewMapMode")]
        public byte? OverviewMapMode { get; set; }

        [JsonPropertyName("panoramaInfo")]
        public PanoramaInfo PanoramaInfo { get; set; }

        [JsonPropertyName("panoramaLookupRadius")]
        public double? PanoramaLookupRadius { get; set; }

        [JsonPropertyName("showCurrentAddress")]
        public bool? ShowCurrentAddress { get; set; }

        [JsonPropertyName("showExitButton")]
        public bool? ShowExitButton { get; set; }

        [JsonPropertyName("showHeadingCompass")]
        public bool? ShowHeadingCompass { get; set; }

        [JsonPropertyName("showProblemReporting")]
        public bool? ShowProblemReporting { get; set; }

        [JsonPropertyName("showZoomButtons")]
        public bool? ShowZoomButtons { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is StreetsideOptions options &&
                   EqualityComparer<bool?>.Default.Equals(DisablePanoramaNavigation, options.DisablePanoramaNavigation) &&
                   EqualityComparer<Geocoordinate>.Default.Equals(LocationToLookAt, options.LocationToLookAt) &&
                   EqualityComparer<byte?>.Default.Equals(OverviewMapMode, options.OverviewMapMode) &&
                   EqualityComparer<PanoramaInfo>.Default.Equals(PanoramaInfo, options.PanoramaInfo) &&
                   EqualityComparer<double?>.Default.Equals(PanoramaLookupRadius, options.PanoramaLookupRadius) &&
                   EqualityComparer<bool?>.Default.Equals(ShowCurrentAddress, options.ShowCurrentAddress) &&
                   EqualityComparer<bool?>.Default.Equals(ShowExitButton, options.ShowExitButton) &&
                   EqualityComparer<bool?>.Default.Equals(ShowHeadingCompass, options.ShowHeadingCompass) &&
                   EqualityComparer<bool?>.Default.Equals(ShowProblemReporting, options.ShowProblemReporting) &&
                   EqualityComparer<bool?>.Default.Equals(ShowZoomButtons, options.ShowZoomButtons);
        }

        public override int GetHashCode()
        {
            System.HashCode hash = new System.HashCode();
            hash.Add(DisablePanoramaNavigation);
            hash.Add(LocationToLookAt);
            hash.Add(OverviewMapMode);
            hash.Add(PanoramaInfo);
            hash.Add(PanoramaLookupRadius);
            hash.Add(ShowCurrentAddress);
            hash.Add(ShowExitButton);
            hash.Add(ShowHeadingCompass);
            hash.Add(ShowProblemReporting);
            hash.Add(ShowZoomButtons);
            return hash.ToHashCode();
        }

        #endregion Methods
    }
}
