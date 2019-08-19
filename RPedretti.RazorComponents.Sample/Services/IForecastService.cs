using RPedretti.RazorComponents.Sample.Data;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Services
{
    public interface IForecastService
    {
        #region Methods

        Task<WeatherForecast[]> GetForecastAsync();

        #endregion Methods
    }
}
