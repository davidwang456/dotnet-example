using System.Collections.ObjectModel;
using System.Windows;
using MyWebApi.Core;
using MyWebApi.Service;
using MyWebApi.Wpf.ViewModels;

namespace MyWebApi.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly WeatherForecastViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new WeatherForecastViewModel(new WeatherForecastService());
            DataContext = _viewModel;
        }

        private void OnLoadClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadForecasts();
        }
    }
}


