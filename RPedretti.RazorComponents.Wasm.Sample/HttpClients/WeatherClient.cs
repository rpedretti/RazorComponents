using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using RPedretti.RazorComponents.Sample.Shared.HttpClients;
using RPedretti.RazorComponents.Sample.Shared.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace RPedretti.RazorComponents.Wasm.Sample.HttpClients
{
    public class WeatherClient : IWeatherClient
    {
        #region Fields

        private readonly string _filePath;
        private readonly IFileProvider _fileProvider;

        #endregion Fields

        #region Constructors

        public WeatherClient(WeatherConfig configuration, IFileProvider fileProvider)
        {
            _filePath = configuration.Filepath;
            _fileProvider = fileProvider;
        }

        #endregion Constructors

        #region Methods

        public async Task<WeatherForecast[]> GetWeather()
        {
            var info = _fileProvider.GetFileInfo(_filePath);
            using(var content = info.CreateReadStream())
            {
                return await JsonSerializer.DeserializeAsync<WeatherForecast[]>(content);
            }
        }

        #endregion Methods
    }
}
