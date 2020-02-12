using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Sensors.AmbientLight;
using RPedretti.RazorComponents.Sensors.Geolocation;
using System;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Sensors
{
    public partial class SensorsPage : IDisposable
    {
        #region Properties

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Inject] private AmbientLightSensorService _ambientLighSensorService { get; set; }
        [Inject] private GeolocationSensorService _geolocationSensorService { get; set; }
        [Inject] private ILogger<SensorsPage> _logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public int Light { get; set; }

        public string? LightError { get; set; }

        public Position? Position { get; set; }

        public bool WaitFirstLoad { get; private set; }

        public bool Watching { get; set; }

        #endregion Properties

        #region Methods

        protected override void OnInitialized()
        {
            _ambientLighSensorService.OnError += OnLightError;
            _ambientLighSensorService.OnReading += OnLightReading;
            _geolocationSensorService.OnPositionError += OnLocationError;
            _geolocationSensorService.OnPositionUpdate += OnLocationReading;
            base.OnInitialized();
        }

        private void DirectionsUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("directions updated");
        }

        protected void OnLightError(object s, string error)
        {
            LightError = error;
            StateHasChanged();
        }

        protected void OnLightReading(object _, int reading)
        {
            Light = reading;
            StateHasChanged();
        }

        protected void OnLocationError(object _, PositionError error)
        {
            _logger.LogError(error.Message);
            StateHasChanged();
        }

        protected void OnLocationReading(object _, Position position)
        {
            Position = position;
            StateHasChanged();
        }

        public void Dispose()
        {
            _ambientLighSensorService.OnError -= OnLightError;
            _ambientLighSensorService.OnReading -= OnLightReading;
            _geolocationSensorService.OnPositionError -= OnLocationError;
            _geolocationSensorService.OnPositionUpdate -= OnLocationReading;
        }

        #endregion Methods
    }
}
