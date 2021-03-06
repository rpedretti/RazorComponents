using RPedretti.RazorComponents.Sample.Shared.Data;
using RPedretti.RazorComponents.Sample.Shared.HttpClients;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.HttpClients
{
    public class WeatherClient : IWeatherClient
    {
        #region Fields

        private readonly HttpClient httpClient;

        #endregion Fields

        #region Constructors

        public WeatherClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        #endregion Constructors

        #region Methods

        public async Task<WeatherForecast[]> GetWeather()
        {
            var response = await httpClient.GetAsync("/sample-data/weather.json");
            var content = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<WeatherForecast[]>(content);
        }

        #endregion Methods
    }
}
