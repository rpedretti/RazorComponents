using Microsoft.AspNetCore.Blazor.Hosting;

namespace RPedretti.RazorComponents.Wasm.Sample
{
    public class Program
    {
        #region Methods

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();

        #endregion Methods
    }
}
