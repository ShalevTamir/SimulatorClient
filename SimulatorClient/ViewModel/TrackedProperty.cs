using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SimulatorClient.ViewModel
{
    public class TrackedProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T newValue, [CallerMemberName]string properyName = null)
        {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
