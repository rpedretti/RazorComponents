using System.Threading.Tasks;
using RPedretti.RazorComponents.Sample.Data;

namespace RPedretti.RazorComponents.Sample.HttpClients
{
    public interface IWeatherClient
    {
        Task<WeatherForecast[]> GetWeather();
    }
}