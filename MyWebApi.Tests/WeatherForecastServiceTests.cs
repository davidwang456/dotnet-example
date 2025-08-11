using NUnit.Framework;
using MyWebApi.Service;

namespace MyWebApi.Tests
{
    [TestFixture]
    public class WeatherForecastServiceTests
    {
        [Test]
        public void GetForecasts_ReturnsFiveItems_WithValidData()
        {
            var service = new WeatherForecastService();
            var list = service.GetForecasts();
            Assert.IsNotNull(list);
            var array = System.Linq.Enumerable.ToArray(list);
            Assert.AreEqual(5, array.Length);
            foreach (var f in array)
            {
                Assert.IsNotNull(f);
                Assert.IsNotNull(f.Summary);
            }
        }
    }
}


