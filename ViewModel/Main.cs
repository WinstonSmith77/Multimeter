using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel.Annotations;

namespace ViewModel
{
    public class Main : INotifyPropertyChanged
    {
        public Main(AccessDevice device)
        {
            device.NewMeasurement += (sender, args) =>
            {
                var value = args.Value;
                IsNegative = value.IsNegative;
                IsAC = value.IsAC;
                IsDC = value.IsDC;
                Value = value.Value;
                Unit = value.Unit;
            };
        }

        private bool _isNegative;
        private bool _isAc;
        private bool _isDc;
        private double _value;

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value.Equals(_value)) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool IsNegative
        {
            get
            {
                return _isNegative;
            }
            set
            {
                if (value.Equals(_isNegative)) return;
                _isNegative = value;
                OnPropertyChanged();
            }
        }

        public bool IsAC
        {
            get
            {
                return _isAc;
            }
            set
            {
                if (value.Equals(_isAc)) return;
                _isAc = value;
                OnPropertyChanged();
            }
        }

        private string _unit;
        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                if (value.Equals(_unit)) return;
                _unit = value;
                OnPropertyChanged();
            }
        }

        public bool IsDC
        {
            get
            {
                return _isDc;
            }
            set
            {
                if (value.Equals(_isDc)) return;
                _isDc = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
