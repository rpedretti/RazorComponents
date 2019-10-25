using Microsoft.JSInterop;
using RPedretti.RazorComponents.Shared;
using System;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities
{
    public abstract class BaseBingMapEntity : BindableBase, IDisposable
    {
        [JsonIgnore]
        internal static IJSRuntime JSRuntime { get; set; }
        public string Type => GetType().Name.ToLower();
        public string Id { get; set; }

        public abstract void Dispose();
    }
}
