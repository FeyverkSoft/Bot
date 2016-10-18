using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class OffsetDateTimeConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            if (dateTime == null)
                return null;

            var timeSpan = parameter as TimeSpan?;
            if (timeSpan == null)
            {
                var timeSpanString = parameter as string;
                if (timeSpanString != null)
                {
                    TimeSpan timeSpanValue;
                    TimeSpan.TryParse(timeSpanString, out timeSpanValue);
                    timeSpan = timeSpanValue;
                }
            }

            if (timeSpan != null)
                dateTime = dateTime.Value.Add(timeSpan.Value);

            return dateTime;
        }
    }
}
