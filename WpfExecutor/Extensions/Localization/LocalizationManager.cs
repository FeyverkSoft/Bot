using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using WpfConverters.Extensions.Commands;

namespace WpfExecutor.Extensions.Localization
{
    /// <summary>
    /// Основной класс для работы с локализацией
    /// </summary>
    public sealed class LocalizationManager : INotifyPropertyChanged
    {
        private LocalizationManager()
        {
        }

        private static LocalizationManager _localizationManager;
        private ICommand _changeCultureCommand;

        public static LocalizationManager Instance => _localizationManager ?? (_localizationManager = new LocalizationManager());

        public event EventHandler<CultureChangedEventArgs> CultureChanged;

        public CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
            set
            {
                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;
                CultureInfo.DefaultThreadCurrentCulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;
                OnCultureChanged(value);
                OnPropertyChanged();
            }
        }

        public IEnumerable<CultureInfo> Cultures => LocalizationProvider?.Cultures ?? Enumerable.Empty<CultureInfo>();

        private ILocalizationProvider LocalizationProvider { get; } = AppContext.Get<ILocalizationProvider>();

        private void OnCultureChanged(CultureInfo culture)
        {
            CultureChanged?.Invoke(this, new CultureChangedEventArgs(culture));
        }

        public ICommand ChangeCultureCommand => _changeCultureCommand ?? (_changeCultureCommand = new DelegateCommand<CultureInfo>(ChangeCulture, CanChangeCulture));

        private bool CanChangeCulture(CultureInfo cultureInfo)
        {
            return cultureInfo != null;
        }

        private void ChangeCulture(CultureInfo culture)
        {
            if (culture == null)
                return;

            CurrentCulture = culture;
        }

        public object Localize(string key)
        {
            if (String.IsNullOrEmpty(key))
                return "[NULL]";
            var localizedValue = LocalizationProvider?.Localize(key);
            return localizedValue ?? $"[{key}]";
        }

        public static string GetString(string key, params object[] args)
        {
            var format = Convert.ToString(Instance.Localize(key));
            return !String.IsNullOrEmpty(format) && args.Length > 0 ? string.Format(format, args) : format;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
