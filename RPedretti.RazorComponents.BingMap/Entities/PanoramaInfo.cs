using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public class PanoramaInfo
    {
        #region Properties

        [JsonPropertyName("cd")]
        public string Cd { get; set; }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj)
        {
            return obj is PanoramaInfo info &&
                   Cd == info.Cd;
        }

        public override int GetHashCode()
        {
            return -1896984974 + EqualityComparer<string>.Default.GetHashCode(Cd);
        }

        #endregion Methods
    }
}
