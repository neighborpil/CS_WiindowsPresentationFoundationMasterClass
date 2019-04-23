using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lec66_WeatherApp.ViewModel.Converters
{
    public class TemperatureToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temperatureString = value.ToString();

            string header = null;
            if (double.TryParse(temperatureString, out double temperature))
            {
                if (temperature < 10)
                    header = $"Low";
                else if (temperature < 20)
                    header = $"Medium";
                else
                    header = $"High";    
                    
            }

            return $"{header} {temperature}º";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "0.0";
        }
    }
}
