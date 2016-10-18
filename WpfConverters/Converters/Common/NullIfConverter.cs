using System;
using System.Globalization;
using System.Linq;

namespace WpfConverters.Converters.Common
{
    public class NullIfConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, parameter))
                return null;
            return value;
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(values.FirstOrDefault(), targetType, parameter, culture);
        }
    }
}
