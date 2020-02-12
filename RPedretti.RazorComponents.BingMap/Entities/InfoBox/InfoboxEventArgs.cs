using Microsoft.AspNetCore.Components.Web;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.InfoBox
{
    public class InfoboxEventArgs
    {
        #region Properties

        [JsonPropertyName("eventName")]
        public string EventName { get; set; }

        [JsonPropertyName("originalEvent")]
        public MouseEventArgs OriginalEvent { get; set; }

        [JsonPropertyName("pageX")]
        public double PageX { get; set; }

        [JsonPropertyName("pageY")]
        public double PageY { get; set; }

        [JsonPropertyName("target")]
        public object Target { get; set; }

        [JsonPropertyName("targetType")]
        public string TargetType { get; set; }

        #endregion Properties
    }
}
