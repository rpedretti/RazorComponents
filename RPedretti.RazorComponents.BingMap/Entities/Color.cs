using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class Color
    {
        #region Properties

        [JsonPropertyName("a")]
        public byte A { get; set; }

        [JsonPropertyName("b")]
        public byte B { get; set; }

        [JsonPropertyName("g")]
        public byte G { get; set; }

        [JsonPropertyName("r")]
        public byte R { get; set; }

        #endregion Properties

        #region Constructors

        public Color()
        {
        }

        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(byte r, byte g, byte b) : this(r, g, b, 255)
        {
        }

        #endregion Constructors

        #region Methods

        public static Color FromSystemColor(System.Drawing.Color color) => new Color
        {
            R = color.R,
            G = color.G,
            B = color.B,
            A = color.A
        };

        public override string ToString() => $"{{R: {R}, G: {G}, B: {B}, A: {A}}}";

        public System.Drawing.Color ToSystemColor() => System.Drawing.Color.FromArgb(A, R, G, B);

        #endregion Methods
    }
}
