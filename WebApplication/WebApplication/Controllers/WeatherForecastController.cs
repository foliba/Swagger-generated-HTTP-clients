using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    /// <summary>
    /// Demo Weather controller which generates weather data on the fly
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Parameter are defined inline, which makes XML documentations much harder <br/>
        /// As 
        /// </summary>
        /// <returns>Generated a list of <see cref="WeatherForecast"/> data</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get(
            [FromQuery]
            [Range(1, int.MaxValue, ErrorMessage = "Page must be a positive non zero integer")] int? amount)
        {
            var rng = new Random();
            return Enumerable.Range(1, amount ?? 5).Select(index => new WeatherForecast
            {
                Date = DateTime.UtcNow.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}