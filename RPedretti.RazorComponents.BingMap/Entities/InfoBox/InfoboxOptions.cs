using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.InfoBox
{
    public class InfoboxOptions
    {
        #region Properties

        [JsonPropertyName("action")]
        public InfoboxAction[] Actions { get; set; }

        [JsonPropertyName("closeDelayTime")]
        public int? CloseDelayTime { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("htmlContent")]
        public string HtmlContent { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("maxHeight")]
        public double? MaxHeight { get; set; }

        [JsonPropertyName("maxWidth")]
        public double? MaxWidth { get; set; }

        [JsonPropertyName("offset")]
        public GeolocatonPoint Offset { get; set; }

        [JsonPropertyName("showCloseButton")]
        public bool? ShowCloseButton { get; set; }

        [JsonPropertyName("showPointer")]
        public bool? ShowPointer { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("visible")]
        public bool? Visible { get; set; }

        [JsonPropertyName("zIndex")]
        public int? ZIndex { get; set; }


        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is InfoboxOptions options &&
                   EqualityComparer<InfoboxAction[]>.Default.Equals(Actions, options.Actions) &&
                   CloseDelayTime == options.CloseDelayTime &&
                   Description == options.Description &&
                   HtmlContent == options.HtmlContent &&
                   EqualityComparer<Location>.Default.Equals(Location, options.Location) &&
                   MaxHeight == options.MaxHeight &&
                   MaxWidth == options.MaxWidth &&
                   EqualityComparer<GeolocatonPoint>.Default.Equals(Offset, options.Offset) &&
                   ShowCloseButton == options.ShowCloseButton &&
                   ShowPointer == options.ShowPointer &&
                   Title == options.Title &&
                   Visible == options.Visible &&
                   ZIndex == options.ZIndex;
        }

        public override int GetHashCode()
        {
            System.HashCode hash = new System.HashCode();
            hash.Add(Actions);
            hash.Add(CloseDelayTime);
            hash.Add(Description);
            hash.Add(HtmlContent);
            hash.Add(Location);
            hash.Add(MaxHeight);
            hash.Add(MaxWidth);
            hash.Add(Offset);
            hash.Add(ShowCloseButton);
            hash.Add(ShowPointer);
            hash.Add(Title);
            hash.Add(Visible);
            hash.Add(ZIndex);
            return hash.ToHashCode();
        }

        #endregion Methods
    }
}
