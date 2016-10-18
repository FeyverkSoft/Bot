using System;
using System.Globalization;

namespace WpfConverters.Converters.DataType
{
    public class DateTimeConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            try
            {
                var dateTime = System.Convert.ToDateTime(value);
                return dateTime;
            }
            catch
            {
                // ignored
            }

            try
            {
                var stringValue = System.Convert.ToString(value);
                if (string.IsNullOrEmpty(stringValue))
                    return null;

                DateTime dateTime;
                if (DateTime.TryParse(stringValue, out dateTime))
                    return dateTime;
                if (DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                    return dateTime;
                if (DateTime.TryParse(stringValue, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime))
                    return dateTime;
                if (DateTime.TryParseExact(stringValue, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateTime))
                    return dateTime;
                if (DateTime.TryParseExact(stringValue, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateTime))
                    return dateTime;
                if (DateTime.TryParseExact(stringValue, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateTime))
                    return dateTime;
                if (DateTime.TryParseExact(stringValue, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateTime))
                    return dateTime;
            }
            catch
            {
                // ignored
            }
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = Convert(value, targetType, parameter, culture) as DateTime?;

            if (targetType == typeof(string))
            {
                var format = parameter as string;
                return dateTime?.ToString(format ?? "yyyy-MM-dd HH:mm:ss");
            }

            return dateTime;
        }
    }
}
