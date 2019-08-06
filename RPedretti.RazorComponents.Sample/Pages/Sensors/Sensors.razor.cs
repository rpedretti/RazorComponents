using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Sensors.Geolocation;
using RPedretti.RazorComponents.Shared.Components;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Pages.Sensors
{
    public class SensorsBase : BaseComponent
    {
        #region Properties

        [Inject]
        protected ILogger<SensorsBase> Logger { get; set; }

        #endregion Properties

        #region Methods

        private void DirectionsUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("directions updated");
        }

        protected Task OnError(string error)
        {
            LightError = error;
            return Task.CompletedTask;
        }

        #endregion Methods

        protected void OnReading(int reading)
        {
            Light = reading;
        }

        protected void StartWatch()
        {
            Watching = true;
        }

        protected void StopWatch()
        {
            Position = null;
            Watching = false;
        }

        public int Light { get; set; }
        public string LightError { get; set; }
        public Position Position { get; set; }
        public bool Watching { get; set; }
        public bool WaitFirstLoad { get; private set; }
    }
}
