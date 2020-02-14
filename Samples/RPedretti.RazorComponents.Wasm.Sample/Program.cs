using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RPedretti.RazorComponents.Sample.Shared.HttpClients;
using RPedretti.RazorComponents.Sample.Shared.Managers;
using RPedretti.RazorComponents.Sample.Shared.Services;
using RPedretti.RazorComponents.Wasm.Sample.HttpClients;
using System.Reflection;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.Sample
{
    public class Program
    {
        #region Methods

        private static void RegisterDependencies(WebAssemblyHostBuilder builder)
        {
            var services = builder.Services;

            var fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var configuration = builder.Configuration
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
            services.AddSingleton<NotificationManager>();
            services.AddSingleton<DownloadManager>();
            services.AddSingleton<BlazorHubConnectionManager>();
            services.AddAmbientLightSensor();
            services.AddGeolocationSensor();
            services.AddModalService();
        }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            RegisterDependencies(builder);

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }

        #endregion Methods
    }
}
