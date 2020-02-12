using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.InfoBox
{
    public class InfoboxAction
    {
        #region Properties

        [JsonPropertyName("label")]
        public string Label { get; set; }

        #endregion Properties
    }
}
