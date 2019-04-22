using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lec66_WeatherApp.Model;
using Newtonsoft.Json;

namespace Lec66_WeatherApp.ViewModel
{
    public class WeatherApi
    {
        public const string API_KEY = "87JFqC8o2DxRhBRso6usswPkbHRTd9A4";
        public const string BASE_URL = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/{0}?apikey={1}&language=ja-jp&metric=true";

        public async Task<Weather> GetWeatherInformationAsync(string locationKey)
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
    }
}
