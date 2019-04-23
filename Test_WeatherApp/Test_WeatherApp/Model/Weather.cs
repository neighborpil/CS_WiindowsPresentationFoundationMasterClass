using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Test_WeatherApp.Annotations;

namespace Test_WeatherApp.Model
{
    public class Degree : INotifyPropertyChanged
    {
        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private string unit;
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged("Unit");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Temperature : INotifyPropertyChanged
    {
        private Degree minimum;
        public Degree Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                OnPropertyChanged("Minimum");
            }
        }

        private Degree maximum;
        public Degree Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value; 
                OnPropertyChanged("Maximum");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class Phase : INotifyPropertyChanged // Day or Night
    {
        private string iconPhrase;

        public string IconPhrase
        {
            get { return iconPhrase; }
            set
            {
                iconPhrase = value;
                OnPropertyChanged("IconPhrase");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DailyForecast : INotifyPropertyChanged
    {
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private Temperature temperature;
        public Temperature Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        private Phase day;
        public Phase Day
        {
            get { return day; }
            set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }

        private Phase night;
        public Phase Night
        {
            get { return night; }
            set
            {
                night = value;
                OnPropertyChanged("Night");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Weather : INotifyPropertyChanged
    {
        //public List<DailyForecast> DailyForecasts { get; set; }
        private List<DailyForecast> dailyForecasts;

        public List<DailyForecast> DailyForecasts
        {
            get { return dailyForecasts; }
            set
            {
                dailyForecasts = value;
                OnPropertyChanged("DailyForecasts");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
