using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RPedretti.RazorComponents.Sensors.AmbientLight;
using RPedretti.RazorComponents.Sensors.Geolocation;

namespace RPedretti.RazorComponents.Sensors.Extensions
{
    public static class SensorsExtensions
    {
        #region Methods

        public static IServiceCollection AddAmbientLightSensor(this IServiceCollection services)
        {
            services.AddSingleton<AmbientLightSensor>();
            return services;
        }

        public static IServiceCollection AddGeolocationSensor(this IServiceCollection services)
        {
            services.AddSingleton<GeolocationSensor>();
            return services;
        }

        #endregion Methods
    }
}
