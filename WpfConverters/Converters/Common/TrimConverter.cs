using System;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    public class TrimConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string)?.Trim();
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(values.FirstOrDefault(), targetType, parameter, culture);
        }
    }
}
