using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Test_WeatherApp.Annotations;
using Test_WeatherApp.Model;
using Test_WeatherApp.ViewModel.Commands;

namespace Test_WeatherApp.ViewModel
{
    public class WeatherVM
    {
        public Weather Weather { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                GetCities();
            }
        }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                GetWeather();
            }
        }

        private IWeatherApi weatherApi;

        public RefreshCommand RefreshCommand { get; set; }

        public WeatherVM()
        {
            Weather = new Weather();
            Cities = new ObservableCollection<City>();
            RefreshCommand = new RefreshCommand(this);

            var builder = new ContainerBuilder();
            //builder.RegisterInstance(SelectedCity);
            builder.RegisterInstance(Weather);
            builder.RegisterInstance(Cities);
            builder.RegisterType<WeatherApi>()
                .As<IWeatherApi>()
                .WithParameter(new ResolvedParameter((pi, context) => pi.ParameterType == typeof(string) && pi.Name == "apikey",
                    (pi, context) => "9RpNOlxt0Vv6KuTWY4AH57m4c2so47sd"));

            var container = builder.Build();
            weatherApi = container.Resolve<IWeatherApi>();
        }

        public async void GetWeather()
        {
            if(SelectedCity == null)
                return;

            var weather = await weatherApi.GetForecastAsync(SelectedCity.Key);
            if (weather == null)
                return;
            Weather.DailyForecasts = weather.DailyForecasts;
        }

        public async void GetCities()
        {
            if (string.IsNullOrWhiteSpace(Query))
                return;

            var cities = await weatherApi.GetCitiesAsync(Query);
            
            Cities.Clear();
            cities.ForEach(c => Cities.Add(c));
        }
    }
}
