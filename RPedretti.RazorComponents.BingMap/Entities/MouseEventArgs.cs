using RPedretti.RazorComponents.BingMap.Entities.Layer;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class MouseEventArgs<T>
    {
        #region Properties

        [JsonPropertyName("eventName")]
        public string EventName { get; set; }

        [JsonPropertyName("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("isSecondary")]
        public bool IsSecondary { get; set; }

        [JsonPropertyName("layer")]
        public BingMapLayer Layer { get; set; }

        [JsonPropertyName("location")]
        public Geocoordinate Location { get; set; }

        [JsonPropertyName("pageX")]
        public double PageX { get; set; }

        [JsonPropertyName("pageY")]
        public double PageY { get; set; }

        [JsonPropertyName("point")]
        public GeolocatonPoint Point { get; set; }

        [JsonPropertyName("target")]
        public T Target { get; set; }

        [JsonPropertyName("targetType")]
        public string TargetType { get; set; }

        [JsonPropertyName("wheelData")]
        public double WheelDelta { get; set; }

        #endregion Properties
    }
}
