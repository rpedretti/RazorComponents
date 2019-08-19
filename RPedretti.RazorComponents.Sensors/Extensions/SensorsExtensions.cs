using Microsoft.Extensions.DependencyInjection;
using RPedretti.RazorComponents.Sensors.AmbientLight;
using RPedretti.RazorComponents.Sensors.Geolocation;

namespace Microsoft.Extensions
{
    public static class SensorsExtensions
    {
        #region Methods

        public static IServiceCollection AddAmbientLightSensor(this IServiceCollection services)
        {
            services.AddSingleton<AmbientLightSensorService>();
            return services;
        }

        public static IServiceCollection AddGeolocationSensor(this IServiceCollection services)
        {
            services.AddSingleton<GeolocationSensorService>();
            return services;
        }

        #endregion Methods
    }
}
