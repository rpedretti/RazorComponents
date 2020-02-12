using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class BingMapsViewConfig
    {
        #region Properties

        [JsonPropertyName("center")]
        public Geocoordinate Center { get; set; }

        [JsonPropertyName("mapTypeId")]
        public string MapTypeId { get; set; }

        [JsonPropertyName("zoom")]
        public int? Zoom { get; set; }

        #endregion Properties

        #region Constructors

        public BingMapsViewConfig()
        {
        }

        public BingMapsViewConfig(BingMapsViewConfig viewConfig)
        {
            Zoom = viewConfig.Zoom;
            MapTypeId = viewConfig.MapTypeId;
        }

        #endregion Constructors

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is BingMapsViewConfig config &&
                   Zoom == config.Zoom &&
                   MapTypeId == config.MapTypeId &&
                   EqualityComparer<Geocoordinate>.Default.Equals(Center, config.Center);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Zoom, MapTypeId, Center);
        }

        #endregion Methods
    }
}
