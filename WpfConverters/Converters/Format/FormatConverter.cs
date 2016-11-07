using System;
using System.Globalization;

namespace WpfConverters.Converters.Format
{
    public class FormatConverter : BaseHybridConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var format = parameter as string;
            if (format == null)
                return null;

            if (values.Length == 0)
                return null;

            if (values.Length == 1 && values[0] == null)
                return null;

            return String.Format(format, values);
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = parameter as string;
            if (format == null)
                return null;

            if (value == null)
                return null;

            if (value as string == string.Empty)
                return null;

            return string.Format(format, value);
        }
    }
}
