using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lec66_WeatherApp.Annotations;
using Lec66_WeatherApp.Model;
using Lec66_WeatherApp.ViewModel.Commands;

namespace Lec66_WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public Weather Weather { get; set; }
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                GetCities();
            }
        }

        public ObservableCollection<City> Cities { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RefreshCommand RefreshCommand { get; set; }

        public WeatherVM()
        {
            Weather = new Weather();
            Cities = new ObservableCollection<City>();
            SelectedCity = new City();

            RefreshCommand = new RefreshCommand(this);
        }

        public async void GetCities()
        {
            var cities = await WeatherApi.GetAutoCompleteAsync(Query);
            Cities.Clear();
            cities.ForEach(city => Cities.Add(city));
        }

        public async void GetWeather()
        {
            if (SelectedCity == null)
                return;

            var weather = await WeatherApi.GetWeatherInformationAsync(SelectedCity.Key);

            if (weather == null)
                return;

            //Weather = weather;
            Weather.DailyForecasts = weather.DailyForecasts;
        }
    }
}
