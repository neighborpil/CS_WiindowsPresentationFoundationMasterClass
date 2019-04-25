using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_WeatherApp.View.Controls
{
    /// <summary>
    /// Interaction logic for DailyForecastControl.xaml
    /// </summary>
    public partial class DailyForecastControl : UserControl
    {

        private DateTime date;
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        private double minimum;
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        private double maximum;
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        private string day;
        public string Day
        {
            get => (string)GetValue(DayProperty);
            set => SetValue(DayProperty, value);
        }

        private string night;
        public string Night
        {
            get => (string)GetValue(NightProperty);
            set => SetValue(NightProperty, value);
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date",
                typeof(DateTime),
                typeof(DailyForecastControl),
                new PropertyMetadata(DateTime.Now, SetDate));

        public static readonly DependencyProperty NightProperty = 
            DependencyProperty.Register("Night",
                typeof(string),
                typeof(DailyForecastControl),
                new PropertyMetadata("", SetNight));

        public static readonly DependencyProperty DayProperty = 
            DependencyProperty.Register("Day",
                typeof(string),
                typeof(DailyForecastControl),
                new PropertyMetadata("", SetDay));

        public static readonly DependencyProperty MaximumProperty = 
            DependencyProperty.Register("Maximum",
                typeof(double),
                typeof(DailyForecastControl),
                new PropertyMetadata(0.0, SetMaximum));

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum",
                typeof(double),
                typeof(DailyForecastControl),
                new PropertyMetadata(0.0, SetMinimum));

        private static void SetDate(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DailyForecastControl control)
            {
                if(DateTime.TryParse(e.NewValue.ToString(), out DateTime date))
                {
                    CultureInfo info = new CultureInfo("ja-JP");
                    control.DateLabel.Content = date.ToString("M", info);
                }
            }
        }

        private static void SetDay(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DailyForecastControl control)
            {
                var text = e.NewValue as string;
                control.DayLabel.Content = text;
            }
        }

        private static void SetNight(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DailyForecastControl control)
            {
                var text = e.NewValue as string;
                control.NightLabel.Content = text;
            }
        }

        private static void SetMaximum(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is DailyForecastControl control)
            {
                if(double.TryParse(e.NewValue.ToString(), out double value))
                {
                    control.MaximumLabel.Content = value.ToString();
                }
            }
        }

        private static void SetMinimum(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DailyForecastControl control)
            {
                if (double.TryParse(e.NewValue.ToString(), out double value))
                {
                    control.MinimumLabel.Content = value.ToString();
                }
            }
        }

        public DailyForecastControl()
        {
            InitializeComponent();
        }
    }
}
