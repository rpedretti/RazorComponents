using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Sample.Data;
using System;
using System.Net.Http;
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
            return await httpClient.GetJsonAsync<WeatherForecast[]>("/sample-data/weather.json");
        }

        #endregion Methods
    }
}
