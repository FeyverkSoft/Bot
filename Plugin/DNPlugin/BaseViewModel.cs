using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Core;
using WpfConverters.Extensions.Commands;

namespace ImgComparer
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

        public event EventHandler<Boolean?> Close;
        private ICommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand<Boolean?>(OnClose));

        private void OnClose(Boolean? b)
        {
            Close?.Invoke(this, b);
        }

        public Version AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version;
    }
}
