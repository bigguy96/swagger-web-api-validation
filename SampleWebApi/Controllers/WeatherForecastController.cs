using System;
using System.Collections.Generic;
using SampleWebApi.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of weather forecasts.
        /// </summary>
        /// <returns>An action result of a weather forecast list</returns>
        /// <response code="200">List of weather forecasts</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedContext), StatusCodes.Status401Unauthorized)]
        public ActionResult< IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var list = new List<WeatherForecast>()
            {
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(1),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                },
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(2),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                },
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(3),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                },
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(4),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                },
                new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(5),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }
            };

            return Ok(list);
        }

        /// <summary>
        /// Add a new weather forecast.
        /// </summary>
        /// <param name="weatherForecast">Weather forecaset object</param>
        /// <returns>Link to new created weather forecast</returns>
        /// <response code="201">Ok Created</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestContext), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedContext), StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<WeatherForecast>> Post([FromBody] WeatherForecast weatherForecast)
        {
            if (weatherForecast == null)
            {
                return BadRequest(new BadRequestContext { ErrorMessage = "Weather forecast cannot be null." });
            }
            
            return CreatedAtRoute("Get", new { weatherForecast.Summary }, weatherForecast);            
        }

        /// <summary>
        /// Update a weather forecast.
        /// </summary>
        /// <param name="weatherForecast">Weather forecaset object</param>
        /// <returns>No content</returns>
        /// <response code="204">No Content</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestContext), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedContext), StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<WeatherForecast>> Put([FromBody] WeatherForecast weatherForecast)
        {
            if (weatherForecast == null)
            {
                return BadRequest(new BadRequestContext { ErrorMessage = "Weather forecast cannot be null." });
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a weather forecast.
        /// </summary>
        /// <param name="id">Weather forecast id</param>
        /// <returns>No content</returns>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestContext), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UnauthorizedContext), StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<WeatherForecast>> Delete(int id)
        {
            if (id <=0)
            {
                return BadRequest(new BadRequestContext { ErrorMessage = "Id must be higher than zero." });
            }

            return NoContent();
        }
    }
}

//https://localhost:44301/swagger/ui/index.html