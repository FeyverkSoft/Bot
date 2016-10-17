using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfExecutor.Extensions.Localization
{
    public class XmlLocalizationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return null;

            var xml = values[1] as string;
            var value = LocalizedStringsHelper.GetLocalizedValue(xml);

            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
