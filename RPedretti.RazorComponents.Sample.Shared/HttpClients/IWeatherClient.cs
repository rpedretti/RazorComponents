using RPedretti.RazorComponents.Sample.Shared.Data;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.HttpClients
{
    public interface IWeatherClient
    {
        #region Methods

        Task<WeatherForecast[]> GetWeather();

        #endregion Methods
    }
}
