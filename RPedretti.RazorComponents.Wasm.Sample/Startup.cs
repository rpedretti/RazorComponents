using System.Text.Json;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RPedretti.RazorComponents.Sample.Shared.HttpClients;
using RPedretti.RazorComponents.Sample.Shared.Services;
using RPedretti.RazorComponents.Wasm.Sample.HttpClients;

namespace RPedretti.RazorComponents.Wasm.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var fileProvider = new EmbeddedFileProvider(this.GetType().Assembly);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(provider: fileProvider, path: "appsettings.json", optional: true, reloadOnChange: false)
                .Build();

            
            services.AddSingleton<IFileProvider>(fileProvider);
            services.AddSingleton(configuration.GetSection("Weather").Get<WeatherConfig>());
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IWeatherClient, WeatherClient>();
            services.AddSingleton<IImdbClient, ImdbClient>();
            services.AddSingleton<IForecastService, ForecastService>();
            services.AddSingleton<IMovieService, ImdbService>();
            services.AddSingleton<IForecastService, ForecastService>();
            services.AddAmbientLightSensor();
            services.AddGeolocationSensor();
            services.AddModalService();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
