﻿using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RPedretti.RazorComponents.BingMap;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.BingMap
{
    public class Program
    {
        #region Methods

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            RegisterDependencies(builder);

            builder.RootComponents.Add<App>("app");

            var host = builder.Build();

            await host.RunAsync();
        }

        private static void RegisterDependencies(WebAssemblyHostBuilder builder)
        {
            var services = builder.Services;

            var fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            var configuration = builder.Configuration
                .AddJsonFile(provider: fileProvider, path: "appsettings.json", optional: true, reloadOnChange: false)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<DevToolService>();
        }

        #endregion Methods
    }
}
