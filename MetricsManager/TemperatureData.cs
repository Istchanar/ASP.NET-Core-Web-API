using System;

namespace MetricsManager
{
    public class TemperatureData
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        /*
        public void CopyData(TemperatureData temperaturedata)
        {
            Date = temperaturedata.Date;
            TemperatureC = temperaturedata.TemperatureC;
        }
        */
    }
}