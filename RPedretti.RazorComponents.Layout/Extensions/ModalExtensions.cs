using Microsoft.Extensions.DependencyInjection;
using RPedretti.RazorComponents.Layout.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions
{
    public static class ModalExtensions
    {
        public static IServiceCollection AddModalService(this IServiceCollection services)
        {
            services.AddSingleton<IModalService, ModalService>();
            return services;
        }
    }
}
