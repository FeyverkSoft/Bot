using System;
using System.Globalization;
using System.Windows.Data;
using W1.AdminTools.WPF.Extensions.Markup;

namespace WpfExecutor.Extensions.Localization
{
    public class LocalizedStringsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return null;

            var localizedStrings = values[1] as LocalizedStrings;
            var value = LocalizedStringsHelper.GetLocalizedValue(localizedStrings, LocalizationManager.Instance.CurrentCulture);

            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
