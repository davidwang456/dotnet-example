using NUnit.Framework;
using MyWebApi.Controllers;
using MyWebApi.Service;

namespace MyWebApi.Tests;

[TestFixture]
public class WeatherForecastControllerTests
{
    [Test]
    public void Get_ReturnsWeatherForecastArray()
    {
        var service = new WeatherForecastService();
        var controller = new WeatherForecastController(service);

        var result = controller.Get();

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(5));

        foreach (var forecast in result)
        {
            Assert.That(forecast, Is.Not.Null);
            Assert.That(forecast.TemperatureC, Is.InRange(-20, 55));
            Assert.That(forecast.Summary, Is.Not.Null);
            Assert.That(forecast.TemperatureF, Is.GreaterThan(0));
        }
    }

    [Test]
    public void Get_ReturnsCorrectTemperatureConversion()
    {
        var service = new WeatherForecastService();
        var controller = new WeatherForecastController(service);

        var result = controller.Get();

        foreach (var forecast in result)
        {
            var expectedFahrenheit = 32 + (int)(forecast.TemperatureC / 0.5556);
            Assert.That(forecast.TemperatureF, Is.EqualTo(expectedFahrenheit));
        }
    }
}