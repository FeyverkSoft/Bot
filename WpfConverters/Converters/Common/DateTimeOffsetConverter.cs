using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class DateTimeOffsetConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            if (dateTime == null)
                return null;

            if (parameter == null)
                return dateTime;

            var timeSpan = parameter as TimeSpan?;
            var timeSpanString = parameter as string;
            if (timeSpanString != null)
            {
                TimeSpan ts;
                if (TimeSpan.TryParse(timeSpanString, out ts))
                    timeSpan = ts;
            }

            if (timeSpan == null)
                return dateTime;

            var result = dateTime.Value.Add(timeSpan.Value);
            return result;
        }
    }
}
