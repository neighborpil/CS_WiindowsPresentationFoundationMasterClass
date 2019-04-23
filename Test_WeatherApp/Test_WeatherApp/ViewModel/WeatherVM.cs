using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_WeatherApp.Model;

namespace Test_WeatherApp.ViewModel
{
    public class WeatherVM
    {
        public Weather Weather { get; set; }

        public WeatherVM()
        {
            Weather = new Weather();
        }
    }
}
