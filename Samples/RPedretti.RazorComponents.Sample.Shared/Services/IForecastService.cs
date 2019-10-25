using RPedretti.RazorComponents.Sample.Shared.Data;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Services
{
    public interface IForecastService
    {
        #region Methods

        Task<WeatherForecast[]> GetForecastAsync();

        #endregion Methods
    }
}
