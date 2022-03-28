using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOMO.Weather.Api.Application.GetWeather.Queries.Interfaces;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            this._weatherService = weatherService;
        }

        [HttpGet]
        [Route("cities/{cityName}")]
        public async Task<ActionResult> GetWeatherForecast(string cityName)
        {
            // Errors are not being handled, I am assuming all operations was successful
            var response = await this._weatherService.GetCurrentWeatherByCityName(cityName);
            if (response.IsSuccessful)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Sorry something went wrong!!" });
        }
    }
}
