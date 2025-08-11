using System.Linq;
using NUnit.Framework;
using MyWebApi.Controllers;
using MyWebApi.Service;

namespace MyWebApi.Tests
{
    [TestFixture]
    public class WeatherForecastControllerTests
    {
        [Test]
        public void Get_ReturnsWeatherForecastArray()
        {
            var service = new WeatherForecastService();
            var controller = new WeatherForecastController(service);

            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());

            foreach (var forecast in result)
            {
                Assert.IsNotNull(forecast);
                Assert.IsTrue(forecast.TemperatureC >= -20 && forecast.TemperatureC <= 55);
                Assert.IsNotNull(forecast.Summary);
                Assert.IsTrue(forecast.TemperatureF > 0);
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
                Assert.AreEqual(expectedFahrenheit, forecast.TemperatureF);
            }
        }
    }
}