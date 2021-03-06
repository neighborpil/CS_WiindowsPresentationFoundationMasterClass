﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lec66_WeatherApp.Annotations;

namespace Lec66_WeatherApp.Model
{
    public class Minimum : INotifyPropertyChanged
    {
        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
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

    public class Maximum : INotifyPropertyChanged
    {
        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        private string _unit;
        public string Unit
        {
            get => _unit;
            set
            {
                _unit = value;
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
        private Minimum _minimum;
        public Minimum Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;
                OnPropertyChanged("Minimum");
            }
        }

        private Maximum _maximum;
        public Maximum Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
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

    public class Day : INotifyPropertyChanged
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

    public class Night : INotifyPropertyChanged
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

        private Day day;
        public Day Day
        {
            get { return day; }
            set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }

        private Night night;
        public Night Night
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
        private List<DailyForecast> _dailyForecasts;
        public List<DailyForecast> DailyForecasts
        {
            get { return _dailyForecasts; }
            set
            {
                _dailyForecasts = value;
                OnPropertyChanged("DailyForecasts");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Weather()
        {
            //if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) // 런타임에서는 안보이고, 디자인에서만 보인다
            //{

            //}

            DailyForecasts = new List<DailyForecast>();
        }
    }
}
