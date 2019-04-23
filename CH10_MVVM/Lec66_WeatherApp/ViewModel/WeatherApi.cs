using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lec66_WeatherApp.Model;
using Newtonsoft.Json;

namespace Lec66_WeatherApp.ViewModel
{
    public class WeatherApi
    {
        public const string API_KEY = "9RpNOlxt0Vv6KuTWY4AH57m4c2so47sd";
        public const string BASE_URL = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/{0}?apikey={1}&language=ja-jp&metric=true";
        public const string BASE_URL_AUTOCOMPLETE = "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}";

        public static async Task<Weather> GetWeatherInformationAsync(string locationKey)
        {
            Weather result = new Weather();

            string url = string.Format(BASE_URL, locationKey, API_KEY);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Weather>(json);
            }
            return result;
        }

        public static async Task<List<City>> GetAutoCompleteAsync(string query)
        {
            List<City> cities = new List<City>();

            string url = string.Format(BASE_URL_AUTOCOMPLETE, API_KEY, query);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                try
                {
                    cities = JsonConvert.DeserializeObject<List<City>>(json);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cant query anymore today");
                }

            }

            return cities;
        }
    }
}
