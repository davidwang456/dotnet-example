using NUnit.Framework;
using MyWebApi.Wpf.ViewModels;
using MyWebApi.Service;

namespace MyWebApi.Tests
{
    [TestFixture]
    public class WeatherForecastViewModelTests
    {
        [Test]
        public void LoadForecasts_PopulatesCollection()
        {
            var vm = new WeatherForecastViewModel(new WeatherForecastService());
            vm.LoadForecasts();
            Assert.IsNotNull(vm.Forecasts);
            Assert.GreaterOrEqual(vm.Forecasts.Count, 1);
        }
    }
}


