using System;
using System.Globalization;

namespace WpfConverters.Converters.Format
{
    public class FormatTimeSpanConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan)) return value;
            var timeSpan = (TimeSpan)value;
            var format = parameter as string;
            if (string.IsNullOrEmpty(format))
                return $"{timeSpan.TotalMinutes:#00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}";
            return string.Format(format, timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }
}
