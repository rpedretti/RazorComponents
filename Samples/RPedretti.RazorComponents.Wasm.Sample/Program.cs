using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RPedretti.RazorComponents.Sample.Shared.HttpClients;
using RPedretti.RazorComponents.Sample.Shared.Services;
using RPedretti.RazorComponents.Wasm.Sample.HttpClients;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.Sample
{
    public class Program
    {
        #region Methods

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            RegisterDependencies(builder.Services, builder.Configuration);

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }

        private static void RegisterDependencies(IServiceCollection services, IConfigurationBuilder configurationBuilder)
        {
            var fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var configuration = configurationBuilder
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

        #endregion Methods
    }
}
