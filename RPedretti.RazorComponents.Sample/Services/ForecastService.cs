using System.Threading.Tasks;
using RPedretti.RazorComponents.Sample.Data;
using RPedretti.RazorComponents.Sample.HttpClients;

namespace RPedretti.RazorComponents.Sample.Services
{
    public partial class ForecastService : IForecastService
    {
        private readonly IWeatherClient weatherClient;

        public ForecastService(IWeatherClient weatherClient)
        {
            this.weatherClient = weatherClient;
        }
        #region Methods

        public async Task<WeatherForecast[]> GetForecastAsync()
        {
            return await weatherClient.GetWeather();
        }

        #endregion Methods
    }
}
