using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace WpfExecutor.Extensions.Localization
{
    public class CultureChangedListener : BaseLocalizationListener, INotifyPropertyChanged
    {
        public CultureChangedListener()
        {
            CurrentCulture = CultureInfo.CurrentCulture;
        }

        private CultureInfo _currentCulture;

        public CultureInfo CurrentCulture
        {
            get { return _currentCulture; }
            private set
            {
                _currentCulture = value;
                OnPropertyChanged();
            }
        }

        protected override void OnCultureChanged(CultureChangedEventArgs e)
        {
            CurrentCulture = e.Culture;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
