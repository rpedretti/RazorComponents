using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Sensors.AmbientLight;
using RPedretti.RazorComponents.Sensors.Geolocation;
using RPedretti.RazorComponents.Shared.Components;
using System;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Sensors
{
    public class SensorsBase : BaseComponent, IDisposable
    {
        #region Properties

        [Inject] protected AmbientLightSensorService ambientLighSensorService { get; set; }
        [Inject] protected GeolocationSensorService geolocationSensorService { get; set; }
        [Inject] protected ILogger<SensorsBase> Logger { get; set; }
        public int Light { get; set; }

        public string LightError { get; set; }

        public Position Position { get; set; }

        public bool WaitFirstLoad { get; private set; }

        public bool Watching { get; set; }

        #endregion Properties

        #region Methods

        protected override void OnInitialized()
        {
            ambientLighSensorService.OnError += OnLightError;
            ambientLighSensorService.OnReading += OnLightReading;
            geolocationSensorService.OnPositionError += OnLocationError;
            geolocationSensorService.OnPositionUpdate += OnLocationReading;
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
            Logger.LogError(error.Message);
            StateHasChanged();
        }

        protected void OnLocationReading(object _, Position position)
        {
            Position = position;
            StateHasChanged();
        }

        public void Dispose()
        {
            ambientLighSensorService.OnError -= OnLightError;
            ambientLighSensorService.OnReading -= OnLightReading;
            geolocationSensorService.OnPositionError -= OnLocationError;
            geolocationSensorService.OnPositionUpdate -= OnLocationReading;
        }

        #endregion Methods
    }
}
