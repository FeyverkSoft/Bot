﻿using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfExecutor.Extensions.Localization
{
    /// <summary>
    /// Конвертер для получения значения выражения привязки в локализации
    /// </summary>
    public class BindingLocalizationConverter : IMultiValueConverter
    {
        private readonly string _keyFormat;
        private readonly string _valueFormat;

        public BindingLocalizationConverter(string keyFormat, string valueFormat)
        {
            _keyFormat = keyFormat;
            _valueFormat = valueFormat;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return null;
            var key = System.Convert.ToString(values[1] ?? "");
            if (!String.IsNullOrEmpty(_keyFormat))
                key = String.Format(_keyFormat, key);
            var value = LocalizationManager.Instance.Localize(key);
            var stringValue = value as string;
            if (stringValue != null)
            {
                var args = ((parameter as IEnumerable)?.Cast<object>() ?? values.Skip(2)).ToArray();
                if (args.Length == 1 && !(args[0] is string) && args[0] is IEnumerable)
                    args = ((IEnumerable) args[0]).Cast<object>().ToArray();
                if (args.Any())
                    stringValue = String.Format(stringValue, args);
                if (!String.IsNullOrEmpty(_valueFormat))
                    stringValue = String.Format(_valueFormat, stringValue);
                return stringValue;
            }
            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}