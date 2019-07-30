using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Sample.Models;
using RPedretti.RazorComponents.Shared.Components;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Components.MoviePoster
{
    public class MoviePosterBase : BaseAccessibleComponent
    {
        #region Fields

        protected bool ImageError;
        protected bool ImageLoaded = false;

        #endregion Fields

        #region Properties

        [Parameter] protected MoviePosterModel Movie { get; set; }
        [Parameter] protected EventCallback OnClick { get; set; }

        #endregion Properties

        #region Methods

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
