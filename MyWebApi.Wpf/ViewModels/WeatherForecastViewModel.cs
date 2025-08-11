using System.Collections.ObjectModel;
using MyWebApi.Core;
using MyWebApi.Service;

namespace MyWebApi.Wpf.ViewModels
{
    public class WeatherForecastViewModel
    {
        private readonly IWeatherForecastService _service;
        public ObservableCollection<WeatherForecast> Forecasts { get; }

        public WeatherForecastViewModel(IWeatherForecastService service)
        {
            _service = service;
            Forecasts = new ObservableCollection<WeatherForecast>();
        }

        public void LoadForecasts()
        {
            Forecasts.Clear();
            foreach (var f in _service.GetForecasts())
            {
                Forecasts.Add(f);
            }
        }
    }
}


