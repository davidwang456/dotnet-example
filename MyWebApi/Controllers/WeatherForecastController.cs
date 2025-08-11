using System.Collections.Generic;
using System.Web.Http;
using MyWebApi.Service;
using MyWebApi.Core;

namespace MyWebApi.Controllers
{
    [RoutePrefix("weatherforecast")]
    public class WeatherForecastController : ApiController
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _service.GetForecasts();
        }
    }
}