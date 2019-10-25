using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace RPedretti.RazorComponents.Wasm.BingMap
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var fileProvider = new EmbeddedFileProvider(this.GetType().Assembly);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(provider: fileProvider, path: "appsettings.json", optional: true, reloadOnChange: false)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
