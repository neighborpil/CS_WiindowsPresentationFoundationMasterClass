using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Test_WeatherApp.ViewModel.Converter
{
    public class TemperatureWithUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temperatureString = value?.ToString();
            string unit = parameter?.ToString();

            return unit.ToLower().Equals("c") ? $"{temperatureString}ºC" : $"{temperatureString}ºF";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "0.0";
        }
    }
}
