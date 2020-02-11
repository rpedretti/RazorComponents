using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.Pushpin
{
    public class PushpinOptions : ICloneable
    {
        #region Properties

        [JsonPropertyName("anchor")]
        public GeolocatonPoint Anchor { get; set; }

        [JsonPropertyName("color")]
        public Color Color { get; set; }

        [JsonPropertyName("cursor")]
        public string Cursor { get; set; }

        [JsonPropertyName("draggable")]
        public bool? Draggable { get; set; }

        [JsonPropertyName("enableClickedStyle")]
        public bool? EnableClickedStyle { get; set; }

        [JsonPropertyName("enableHoverStyle")]
        public bool? EnableHoverStyle { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("roundClickableArea")]
        public bool? RoundClickableArea { get; set; }

        [JsonPropertyName("subTitle")]
        public string SubTitlte { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("textOffset")]
        public GeolocatonPoint TextOffset { get; set; }

        [JsonPropertyName("title")]
        public string Titlte { get; set; }

        [JsonPropertyName("visible")]
        public bool? Visible { get; set; }

        #endregion Properties

        #region Methods

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return obj is PushpinOptions options &&
                   EqualityComparer<GeolocatonPoint>.Default.Equals(Anchor, options.Anchor) &&
                   EqualityComparer<Color>.Default.Equals(Color, options.Color) &&
                   Cursor == options.Cursor &&
                   EqualityComparer<bool?>.Default.Equals(Draggable, options.Draggable) &&
                   EqualityComparer<bool?>.Default.Equals(EnableClickedStyle, options.EnableClickedStyle) &&
                   EqualityComparer<bool?>.Default.Equals(EnableHoverStyle, options.EnableHoverStyle) &&
                   Icon == options.Icon &&
                   EqualityComparer<bool?>.Default.Equals(RoundClickableArea, options.RoundClickableArea) &&
                   SubTitlte == options.SubTitlte &&
                   Text == options.Text &&
                   EqualityComparer<GeolocatonPoint>.Default.Equals(TextOffset, options.TextOffset) &&
                   Titlte == options.Titlte &&
                   EqualityComparer<bool?>.Default.Equals(Visible, options.Visible);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Anchor);
            hash.Add(Color);
            hash.Add(Cursor);
            hash.Add(Draggable);
            hash.Add(EnableClickedStyle);
            hash.Add(EnableHoverStyle);
            hash.Add(Icon);
            hash.Add(RoundClickableArea);
            hash.Add(SubTitlte);
            hash.Add(Text);
            hash.Add(TextOffset);
            hash.Add(Titlte);
            hash.Add(Visible);
            return hash.ToHashCode();
        }

        #endregion Methods
    }
}
