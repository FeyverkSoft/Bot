using System;
using System.ComponentModel;

namespace WpfExecutor.Extensions.Localization
{
    /// <summary>
    /// Слушатель изменения культуры при локализации по ключу
    /// </summary>
    public class KeyLocalizationListener : BaseLocalizationListener, INotifyPropertyChanged
    {
        public KeyLocalizationListener(string key, object[] args, string valueFormat)
        {
            _key = key;
            _args = args;
            _valueFormat = valueFormat;
        }

        private readonly string _key;

        private readonly object[] _args;

        private readonly string _valueFormat;

        public object Value
        {
            get
            {
                var value = LocalizationManager.Instance.Localize(_key);
                var stringValue = value as string;
                if (stringValue != null)
                {
                    if (_args != null && _args.Length > 0)
                        stringValue = string.Format(stringValue, _args);
                    if (!String.IsNullOrEmpty(_valueFormat))
                        stringValue = String.Format(_valueFormat, stringValue);
                    return stringValue;
                }
                return value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnCultureChanged(CultureChangedEventArgs e)
        {
            // Уведомляем привязку об изменении строки
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }
}
