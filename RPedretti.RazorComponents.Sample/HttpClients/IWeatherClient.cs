using RPedretti.RazorComponents.Sample.Data;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.HttpClients
{
    public interface IWeatherClient
    {
        #region Methods

        Task<WeatherForecast[]> GetWeather();

        #endregion Methods
    }
}
