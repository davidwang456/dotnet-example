using MyWebApi.Core;
using Newtonsoft.Json;

namespace MyWebApi.Service;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetForecasts();
}

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        var random = new Random();
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = random.Next(-20, 55),
            Summary = Summaries[random.Next(Summaries.Length)]
        }).ToArray();
        var json = JsonConvert.SerializeObject(forecasts);
        return JsonConvert.DeserializeObject<List<WeatherForecast>>(json) ?? [];
    }
}