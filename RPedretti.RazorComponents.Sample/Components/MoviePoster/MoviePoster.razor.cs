using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Sample.Models;
using RPedretti.RazorComponents.Shared.Components;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Components.MoviePoster
{
    public class MoviePosterBase : BaseAccessibleComponent
    {
        #region Fields

        protected bool ImageError = false;
        protected bool ImageLoaded = false;

        #endregion Fields

        #region Properties

        [Inject] protected ILogger<MoviePosterBase> Logger { get; set; }
        [Parameter] protected MoviePosterModel Movie { get; set; }
        [Parameter] protected EventCallback OnClick { get; set; }

        #endregion Properties

        #region Methods

        protected override async Task OnParametersSetAsync()
        {
            ImageError = false;
            Logger.LogDebug("movie parameter set");
            await base.OnParametersSetAsync();
        }

        protected async Task HandleClick()
        {
            await OnClick.InvokeAsync(null);
        }

        protected void UpdateError()
        {
            ImageError = true;
            ImageLoaded = true;
        }

        protected void UpdateLoader()
        {
            ImageLoaded = true;
        }

        #endregion Methods
    }
}
