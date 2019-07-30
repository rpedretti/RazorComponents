using System;

namespace RPedretti.RazorComponents.Sample.Data
{
    public class WeatherForecast
    {
        #region Properties

        public DateTime Date { get; set; }
        public int RainAmmount { get; set; }
        public int RainChangePercent { get; set; }
        public int Temperature { get; set; }

        #endregion Properties
    }
}
