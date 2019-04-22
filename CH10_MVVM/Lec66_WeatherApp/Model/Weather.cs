using System;
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
    //public class Headline
    //{
    //    public DateTime EffectiveDate { get; set; }
    //    public int EffectiveEpochDate { get; set; }
    //    public int Severity { get; set; }
    //    public string Text { get; set; }
    //    public string Category { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public int EndEpochDate { get; set; }
    //    public string MobileLink { get; set; }
    //    public string Link { get; set; }
    //}

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

        private int _unitType;
        public int UnitType
        {
            get => _unitType;
            set
            {
                _unitType = value;
                OnPropertyChanged("UnitType");
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

        private int _unitType;
        public int UnitType
        {
            get => _unitType;
            set
            {
                _unitType = value;
                OnPropertyChanged("UnitType");
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

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public Temperature Temperature { get; set; }
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

        //public Headline Headline { get; set; }
        //public List<DailyForecast> DailyForecasts { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Weather()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) // 런타임에서는 안보이고, 디자인에서만 보인다
            {
                DailyForecasts = new List<DailyForecast>();
                for (int i = 0; i < 3; i++)
                {
                    DailyForecast dailyForecast = new DailyForecast()
                    {
                        Date = DateTime.Now.AddDays(-1),
                        Temperature = new Temperature
                        {
                            Maximum = new Maximum
                            {
                                Unit = "Celsius",
                                Value = 30,
                                UnitType = 1
                            },
                            Minimum = new Minimum
                            {
                                Unit = "Celsius",
                                Value = 10,
                                UnitType = 1
                            }
                        }
                    };
                    DailyForecasts.Add(dailyForecast);
                }
            }
        }
    }
}
