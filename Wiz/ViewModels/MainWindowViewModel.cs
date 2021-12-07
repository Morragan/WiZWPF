using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using Wiz.Helpers;

namespace Wiz.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region UI Properties
        private int _brightness = 10;
        public int Brightness
        {
            get { return _brightness; }
            set
            {
                var oldValue = _brightness;
                _brightness = value;
                OnPropertyChanged("Brightness", oldValue, value);
            }
        }

        private bool _enabled = false;
        public bool Enabled
        {
            get { return _enabled; }
            set 
            {
                var oldValue = _enabled;
                _enabled = value;
                OnPropertyChanged("Enabled", oldValue, value);
            }
        }

        private int _pingInterval = 100;
        public int PingInterval
        {
            get { return _pingInterval; }
            set
            {
                if (value < 1) return;
                var oldValue = _pingInterval;
                _pingInterval = value;
                OnPropertyChanged("PingInterval", oldValue, value);
                ChangePingInterval(value);
            }
        }

        private byte _r = 255;
        private byte _g = 255;
        private byte _b = 255;
        public Color LightColor
        {
            get { return Color.FromRgb(_r, _g, _b); }
            set
            {
                var oldValueR = _r;
                var oldValueG = _g;
                var oldValueB = _b;

                _r = value.R;
                _g = value.G;
                _b = value.B;

                OnPropertyChanged("Red", oldValueR, value.R);
                OnPropertyChanged("Green", oldValueG, value.G);
                OnPropertyChanged("Blue", oldValueB, value.B);
            }
        }

        public List<Scene> Scenes { get; set; } = CommandHelper.scenesList;
        private int _sceneId = 12;
        public Scene Scene
        {
            get { return Scenes!.Find(scene => scene!.ID == _sceneId); }
            set 
            {
                var oldValue = _sceneId;
                _sceneId = value.ID;
                OnPropertyChanged("Scene", oldValue, value.ID);
            }
        }

        private int _temperature = 2200;
        public int Temperature
        {
            get { return _temperature; }
            set
            {
                var oldValue = _temperature;
                _temperature = value;
                OnPropertyChanged("Temperature", oldValue, value);
            }
        }

        private int _speed;

        public int Speed
        {
            get { return _speed; }
            set 
            {
                var oldValue = _speed;
                _speed = value;
                OnPropertyChanged("Speed", oldValue, value);
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name, object oldValue, object newValue)
        {
            var handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, null, null);
            handler?.Invoke(this, new PropertyChangedEventArgsWithValues(name, oldValue, newValue));
            App.Instance.ParamsToUpdate[name] = newValue;
            if (name != "Enabled") Enabled = true;
        }

        private void ChangePingInterval(int milliseconds)
        {
            App.Instance.Timer.Stop();
            App.Instance.Timer.Interval = TimeSpan.FromMilliseconds(milliseconds);
            App.Instance.Timer.Start();
        }
    }

    internal class PowerButtonForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = (bool)value;
            if (enabled) return new SolidColorBrush(Color.FromRgb(255, 255, 255));
            else return new SolidColorBrush(Color.FromRgb(130, 132, 133));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class PowerButtonBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = (bool)value;
            if (enabled) return new SolidColorBrush(Color.FromRgb(130, 132, 133));
            else return new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class PowerButtonOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enabled = (bool)value;
            if (enabled) return 0;
            else return 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
