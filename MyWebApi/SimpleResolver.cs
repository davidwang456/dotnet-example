using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using MyWebApi.Service;

namespace MyWebApi
{
    public class SimpleResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(Controllers.WeatherForecastController))
            {
                return new Controllers.WeatherForecastController(new WeatherForecastService());
            }
            if (serviceType == typeof(IWeatherForecastService))
            {
                return new WeatherForecastService();
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new object[0];
        }

        public void Dispose()
        {
        }
    }
}


