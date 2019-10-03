using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Sample.Shared.Models;
using RPedretti.RazorComponents.Shared.Components;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Components.MoviePoster
{
    public class MoviePosterBase : BaseAccessibleComponent
    {
        #region Fields

        protected bool ImageError = false;
        protected bool ImageLoaded = false;

        #endregion Fields

        #region Properties

        [Inject] protected ILogger<MoviePosterBase> Logger { get; set; }
        [Parameter] public MoviePosterModel Movie { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        #endregion Properties

        #region Methods

        protected async Task HandleClick()
        {
            await OnClick.InvokeAsync(null);
        }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            var isPresent = parameters.TryGetValue<MoviePosterModel>(nameof(Movie), out var movie);
            if (isPresent)
            {
                if (movie.Id != Movie?.Id)
                {
                    ImageLoaded = false;
                    ImageError = false;
                }
            }
            return base.SetParametersAsync(parameters);
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
