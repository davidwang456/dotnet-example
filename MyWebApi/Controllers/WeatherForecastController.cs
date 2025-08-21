using Microsoft.AspNetCore.Mvc;
using MyWebApi.Service;
using MyWebApi.Core;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(IWeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _service.GetForecasts();
    }
}