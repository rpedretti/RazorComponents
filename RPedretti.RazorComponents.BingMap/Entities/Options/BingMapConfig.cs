using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class BingMapConfig : BingMapsViewConfig
    {
        #region Properties

        [JsonPropertyName("allowHidingLabelsOfRoad")]
        public bool? AllowHidingLabelsOfRoad { get; set; }

        [JsonPropertyName("allowInfoboxOverflow")]
        public bool? AllowInfoboxOverflow { get; set; }

        [JsonPropertyName("backgroundColor")]
        public Color BackgroundColor { get; set; } = new Color(0xFF, 0xEA, 0x8E, 0xE1);

        [JsonPropertyName("credentials")]
        public string Credentials { get; set; }

        [JsonPropertyName("disableBirdseye")]
        public bool? DisableBirdseye { get; set; }

        [JsonPropertyName("disableKeyboardInput")]
        public bool? DisableKeyboardInput { get; set; }

        [JsonPropertyName("disableMapTypeSelectorMouseOver")]
        public bool? DisableMapTypeSelectorMouseOver { get; set; }

        [JsonPropertyName("disablePanning")]
        public bool? DisablePanning { get; set; }

        [JsonPropertyName("disableScrollWheelZoom")]
        public bool? DisableScrollWheelZoom { get; set; }

        [JsonPropertyName("disableStreetside")]
        public bool? DisableStreetside { get; set; }

        [JsonPropertyName("disableStreetsideAutoCoverage")]
        public bool? DisableStreetsideAutoCoverage { get; set; }

        [JsonPropertyName("disableZooming")]
        public bool? DisableZooming { get; set; }

        [JsonPropertyName("enableClickableLogo")]
        public bool? EnableClickableLogo { get; set; }

        [JsonPropertyName("enableCORS")]
        public bool? EnableCORS { get; set; }

        [JsonPropertyName("enableHighDpi")]
        public bool? EnableHighDpi { get; set; }

        [JsonPropertyName("enableInertia")]
        public bool? EnableInertia { get; set; }

        [JsonPropertyName("liteMode")]
        public bool? LiteMode { get; set; }

        [JsonPropertyName("maxBounds")]
        public LocationRectangle MaxBounds { get; set; }

        [JsonPropertyName("maxZoom")]
        public int? MaxZoom { get; set; }

        [JsonPropertyName("minZoom")]
        public int? MinZoom { get; set; }

        [JsonPropertyName("navigationBarMode")]
        public byte? NavigationBarMode { get; set; }

        [JsonPropertyName("navigationBarOrientation")]
        public byte? NavigationBarOrientation { get; set; }

        [JsonPropertyName("showBreadcrumb")]
        public bool? ShowBreadcrumb { get; set; }

        [JsonPropertyName("showDashboard")]
        public bool? ShowDashboard { get; set; }

        [JsonPropertyName("showLocateMeButton")]
        public bool? ShowLocateMeButton { get; set; }

        [JsonPropertyName("showMapTypeSelector")]
        public bool? ShowMapTypeSelector { get; set; }

        [JsonPropertyName("showScalebar")]
        public bool? ShowScalebar { get; set; }

        [JsonPropertyName("showTermsLink")]
        public bool? ShowTermsLink { get; set; }

        [JsonPropertyName("showTrafficButton")]
        public bool? ShowTrafficButton { get; set; }

        [JsonPropertyName("showZoomButtons")]
        public bool? ShowZoomButtons { get; set; }

        [JsonPropertyName("streetsideOptions")]
        public StreetsideOptions StreetsideOptions { get; set; }

        [JsonPropertyName("supportedMapTypes")]
        public string[] SupportedMapTypes { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is BingMapConfig config &&
                   base.Equals(obj) &&
                   EqualityComparer<bool?>.Default.Equals(AllowHidingLabelsOfRoad, config.AllowHidingLabelsOfRoad) &&
                   EqualityComparer<bool?>.Default.Equals(AllowInfoboxOverflow, config.AllowInfoboxOverflow) &&
                   EqualityComparer<Color>.Default.Equals(BackgroundColor, config.BackgroundColor) &&
                   Credentials == config.Credentials &&
                   EqualityComparer<bool?>.Default.Equals(DisableBirdseye, config.DisableBirdseye) &&
                   EqualityComparer<bool?>.Default.Equals(DisableKeyboardInput, config.DisableKeyboardInput) &&
                   EqualityComparer<bool?>.Default.Equals(DisableMapTypeSelectorMouseOver, config.DisableMapTypeSelectorMouseOver) &&
                   EqualityComparer<bool?>.Default.Equals(DisablePanning, config.DisablePanning) &&
                   EqualityComparer<bool?>.Default.Equals(DisableScrollWheelZoom, config.DisableScrollWheelZoom) &&
                   EqualityComparer<bool?>.Default.Equals(DisableStreetside, config.DisableStreetside) &&
                   EqualityComparer<bool?>.Default.Equals(DisableStreetsideAutoCoverage, config.DisableStreetsideAutoCoverage) &&
                   EqualityComparer<bool?>.Default.Equals(DisableZooming, config.DisableZooming) &&
                   EqualityComparer<bool?>.Default.Equals(EnableClickableLogo, config.EnableClickableLogo) &&
                   EqualityComparer<bool?>.Default.Equals(EnableCORS, config.EnableCORS) &&
                   EqualityComparer<bool?>.Default.Equals(EnableHighDpi, config.EnableHighDpi) &&
                   EqualityComparer<bool?>.Default.Equals(EnableInertia, config.EnableInertia) &&
                   EqualityComparer<bool?>.Default.Equals(LiteMode, config.LiteMode) &&
                   EqualityComparer<LocationRectangle>.Default.Equals(MaxBounds, config.MaxBounds) &&
                   EqualityComparer<int?>.Default.Equals(MaxZoom, config.MaxZoom) &&
                   EqualityComparer<int?>.Default.Equals(MinZoom, config.MinZoom) &&
                   EqualityComparer<byte?>.Default.Equals(NavigationBarMode, config.NavigationBarMode) &&
                   EqualityComparer<byte?>.Default.Equals(NavigationBarOrientation, config.NavigationBarOrientation) &&
                   EqualityComparer<bool?>.Default.Equals(ShowBreadcrumb, config.ShowBreadcrumb) &&
                   EqualityComparer<bool?>.Default.Equals(ShowDashboard, config.ShowDashboard) &&
                   EqualityComparer<bool?>.Default.Equals(ShowLocateMeButton, config.ShowLocateMeButton) &&
                   EqualityComparer<bool?>.Default.Equals(ShowMapTypeSelector, config.ShowMapTypeSelector) &&
                   EqualityComparer<bool?>.Default.Equals(ShowScalebar, config.ShowScalebar) &&
                   EqualityComparer<bool?>.Default.Equals(ShowTermsLink, config.ShowTermsLink) &&
                   EqualityComparer<bool?>.Default.Equals(ShowTrafficButton, config.ShowTrafficButton) &&
                   EqualityComparer<bool?>.Default.Equals(ShowZoomButtons, config.ShowZoomButtons) &&
                   EqualityComparer<StreetsideOptions>.Default.Equals(StreetsideOptions, config.StreetsideOptions) &&
                   EqualityComparer<string[]>.Default.Equals(SupportedMapTypes, config.SupportedMapTypes);
        }

        public override int GetHashCode()
        {
            System.HashCode hash = new System.HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(AllowHidingLabelsOfRoad);
            hash.Add(AllowInfoboxOverflow);
            hash.Add(BackgroundColor);
            hash.Add(Credentials);
            hash.Add(DisableBirdseye);
            hash.Add(DisableKeyboardInput);
            hash.Add(DisableMapTypeSelectorMouseOver);
            hash.Add(DisablePanning);
            hash.Add(DisableScrollWheelZoom);
            hash.Add(DisableStreetside);
            hash.Add(DisableStreetsideAutoCoverage);
            hash.Add(DisableZooming);
            hash.Add(EnableClickableLogo);
            hash.Add(EnableCORS);
            hash.Add(EnableHighDpi);
            hash.Add(EnableInertia);
            hash.Add(LiteMode);
            hash.Add(MaxBounds);
            hash.Add(MaxZoom);
            hash.Add(MinZoom);
            hash.Add(NavigationBarMode);
            hash.Add(NavigationBarOrientation);
            hash.Add(ShowBreadcrumb);
            hash.Add(ShowDashboard);
            hash.Add(ShowLocateMeButton);
            hash.Add(ShowMapTypeSelector);
            hash.Add(ShowScalebar);
            hash.Add(ShowTermsLink);
            hash.Add(ShowTrafficButton);
            hash.Add(ShowZoomButtons);
            hash.Add(StreetsideOptions);
            hash.Add(SupportedMapTypes);
            return hash.ToHashCode();
        }

        #endregion Methods
    }
}
