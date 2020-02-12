using Microsoft.JSInterop;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.Pushpin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap
{
    public sealed class DevToolService
    {
        #region Properties



        public IJSRuntime JSRuntime { get; }

        #endregion Properties

        #region Constructors

        public DevToolService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        #endregion Constructors

        #region Methods

        public async Task<List<BingMapPushpin>> GetPushpins(int ammount, LocationRectangle bounds = null, PushpinOptions options = null)
        {
            return await JSRuntime.InvokeAsync<List<BingMapPushpin>>("rpedrettiBlazorComponents.bingMap.devTools.getPushpins", ammount, bounds, options);
        }

        #endregion Methods
    }
}
