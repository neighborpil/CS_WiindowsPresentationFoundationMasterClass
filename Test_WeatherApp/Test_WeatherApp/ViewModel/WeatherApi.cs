using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Test_WeatherApp.Model;

namespace Test_WeatherApp.ViewModel
{
    public interface IWeatherApi
    {
        Task<Weather> GetForecast(string key);
        Task<List<City>> GetCities(string key);
    }

    public class WeatherApi : IWeatherApi
    {
        private readonly string _apiKey;
        public const string FORECASE_BASE_URL = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/{1}?apikey={0}&language=ja-jp&metric=true";
        public const string AUTOCOMPLETE_BASE_URL =
            "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}";

        public WeatherApi(string apikey)
        {
            _apiKey = apikey;
        }

        public async Task<Weather> GetForecast(string key)
        {
            Weather weather = null;

            string url = string.Format(FORECASE_BASE_URL, _apiKey, key);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                weather = JsonConvert.DeserializeObject<Weather>(json);
            }

            return weather;
        }

        public async Task<List<City>> GetCities(string key)
        {
            List<City> cities = new List<City>();
            string url = string.Format(AUTOCOMPLETE_BASE_URL, _apiKey, key);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                List<City> newCities = JsonConvert.DeserializeObject<List<City>>(json);
                if (newCities != null)
                    cities.AddRange(newCities);
            }

            return cities;
        }
    }
}
