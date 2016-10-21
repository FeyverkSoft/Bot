using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfExecutor.Model
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            StaticPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
