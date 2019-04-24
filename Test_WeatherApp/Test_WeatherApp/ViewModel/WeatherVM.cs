using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Test_WeatherApp.Annotations;
using Test_WeatherApp.Model;

namespace Test_WeatherApp.ViewModel
{
    public class WeatherVM 
    {
        public Weather Weather { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set { selectedCity = value; }
        }

        //private IWeatherApi weatherApi;

        public WeatherVM()
        {
            Weather = new Weather();
            Cities = new ObservableCollection<City>();
            



            //Weather.DailyForecasts = new List<DailyForecast>();
            //for (int i = 0; i < 5; i++)
            //{
            //    var forecast = new DailyForecast
            //    {
            //        Date = DateTime.Now.AddDays(i),
            //        Day = new Phase { IconPhrase = "tes" },
            //        Night = new Phase { IconPhrase = "ni" },
            //        Temperature = new Temperature
            //        {
            //            Minimum = new Degree { Value = 3.0 + i },
            //            Maximum = new Degree { Value = 30.3 - i }
            //        }
            //    };

            //    Weather.DailyForecasts?.Add(forecast);
            //}
        }

    }
}
