using Microsoft.JSInterop;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.Pushpin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap
{
    public sealed class DevToolService
    {
        public DevToolService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        public IJSRuntime JSRuntime { get; }

        public async Task<List<BingMapPushpin>> GetPushpins(int ammount, LocationRectangle bounds = null, PushpinOptions options = null)
        {
            var p = await JSRuntime.InvokeAsync<List<BingMapPushpin>>("rpedrettiBlazorComponents.bingMap.devTools.getPushpins", ammount, bounds, options);
            return p;
         }
    }
}
