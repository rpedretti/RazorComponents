using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.RazorComponents.Sample.Shared.HttpClients;
using RPedretti.RazorComponents.Sample.Shared.Services;
using RPedretti.RazorComponents.Wasm.Sample.HttpClients;

namespace RPedretti.RazorComponents.Wasm.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IWeatherClient, WeatherClient>();
            services.AddSingleton<IImdbClient, ImdbClient>();
            services.AddSingleton<IForecastService, ForecastService>();
            services.AddSingleton<IMovieService, ImdbService>();
            services.AddSingleton<IForecastService, ForecastService>();
            services.AddAmbientLightSensor();
            services.AddGeolocationSensor();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
