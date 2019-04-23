using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_WeatherApp.Model;

namespace Test_WeatherApp.ViewModel
{
    public class ModelApi
    {
        private readonly string _apiKey;
        public const string FORECASE_BASE_URL = "http://dataservice.accuweather.com/forecasts/v1/daily/5day/{1}?apikey={0}&language=ja-jp&metric=true";

        public ModelApi(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<Weather> GetForecast(string key)
        {

        }
    }
}
