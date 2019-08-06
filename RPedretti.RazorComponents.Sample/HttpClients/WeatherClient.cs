using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Sample.Data;

namespace RPedretti.RazorComponents.Sample.HttpClients
{
    public class WeatherClient : IWeatherClient
    {
        private readonly HttpClient httpClient;

        public WeatherClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<WeatherForecast[]> GetWeather()
        {
            return await httpClient.GetJsonAsync<WeatherForecast[]>("/sample-data/weather.json");
        }
    }
}