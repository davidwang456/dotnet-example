using NUnit.Framework;
using MyWebApi.Wpf.ViewModels;
using MyWebApi.Service;

namespace MyWebApi.Tests;

[TestFixture]
public class WeatherForecastViewModelTests
{
    [Test]
    public void LoadForecasts_PopulatesCollection()
    {
        var vm = new WeatherForecastViewModel(new WeatherForecastService());
        vm.LoadForecasts();
        Assert.That(vm.Forecasts, Is.Not.Null);
        Assert.That(vm.Forecasts.Count, Is.GreaterThanOrEqualTo(1));
    }
}


