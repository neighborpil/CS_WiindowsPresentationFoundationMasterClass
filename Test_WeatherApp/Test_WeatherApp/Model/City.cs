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
    public class Region : INotifyPropertyChanged
    {
        private string localizedName;
        public string LocalizedName
        {
            get => localizedName;
            set
            {
                localizedName = value;
                OnPropertyChanged("LocalizedName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class City : INotifyPropertyChanged
    {
        private string key;
        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged("Key");
            }
        }

        private string localizedName;
        public string LocalizedName
        {
            get => localizedName;
            set
            {
                localizedName = value;
                OnPropertyChanged("LocalizedName");
            }
        }

        private Region country;
        public Region Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        private Region administrativeArea;
        public Region AdministrativeArea
        {
            get => administrativeArea;
            set
            {
                administrativeArea = value;
                OnPropertyChanged("AdministrativeArea");
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
