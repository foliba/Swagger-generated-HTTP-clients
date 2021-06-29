using System;

namespace WebApplication
{
    /// <summary>
    ///     We don't add XML comments here to showcase that those information are missing in swagger when not set
    /// </summary>
    public class WeatherForecast
    {
#pragma warning disable 1591
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

        public string Summary { get; set; }
#pragma warning restore 1591
    }
}