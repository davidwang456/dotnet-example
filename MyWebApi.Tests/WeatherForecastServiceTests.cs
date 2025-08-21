using NUnit.Framework;
using MyWebApi.Service;

namespace MyWebApi.Tests;

[TestFixture]
public class WeatherForecastServiceTests
{
    [Test]
    public void GetForecasts_ReturnsFiveItems_WithValidData()
    {
        var service = new WeatherForecastService();
        var list = service.GetForecasts();
        Assert.That(list, Is.Not.Null);
        var array = list.ToArray();
        Assert.That(array, Has.Length.EqualTo(5));
        foreach (var f in array)
        {
            Assert.That(f, Is.Not.Null);
            Assert.That(f.Summary, Is.Not.Null);
        }
    }
}


