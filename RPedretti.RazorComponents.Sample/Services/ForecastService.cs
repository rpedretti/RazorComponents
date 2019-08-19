using RPedretti.RazorComponents.Sample.Data;
using RPedretti.RazorComponents.Sample.HttpClients;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Services
{
    public partial class ForecastService : IForecastService
    {
        #region Fields

        private readonly IWeatherClient weatherClient;

        #endregion Fields

        #region Constructors

        public ForecastService(IWeatherClient weatherClient)
        {
            this.weatherClient = weatherClient;
        }

        #endregion Constructors

        #region Methods

        public async Task<WeatherForecast[]> GetForecastAsync()
        {
            return await weatherClient.GetWeather();
        }

        #endregion Methods
    }
}
