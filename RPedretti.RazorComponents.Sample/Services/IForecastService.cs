using System.Threading.Tasks;
using RPedretti.RazorComponents.Sample.Data;

namespace RPedretti.RazorComponents.Sample.Services
{
    public interface IForecastService
    {
        #region Methods

        Task<WeatherForecast[]> GetForecastAsync();

        #endregion Methods
    }
}
