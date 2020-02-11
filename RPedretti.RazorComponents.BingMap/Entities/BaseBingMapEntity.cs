using Microsoft.JSInterop;
using RPedretti.RazorComponents.Shared;
using System;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public abstract class BaseBingMapEntity : BindableBase, IDisposable
    {
        #region Properties

        [JsonIgnore]
        internal static IJSRuntime JSRuntime { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type => GetType().Name.ToLower();

        #endregion Properties

        #region Methods

        public abstract void Dispose();

        #endregion Methods
    }
}
