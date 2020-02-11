using Microsoft.Extensions.DependencyInjection;
using RPedretti.RazorComponents.Layout.Services;

namespace Microsoft.Extensions
{
    public static class ModalExtensions
    {
        #region Methods

        public static IServiceCollection AddModalService(this IServiceCollection services)
        {
            services.AddSingleton<IModalService, ModalService>();
            return services;
        }

        #endregion Methods
    }
}
