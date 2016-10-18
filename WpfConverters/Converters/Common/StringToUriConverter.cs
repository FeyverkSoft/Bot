using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class StringToUriConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str))
                return null;

            var uriKind = parameter as UriKind?;

            try
            {
                var uri = uriKind == null ? new Uri(str) : new Uri(str, uriKind.Value);
                return uri;
            }
            catch (UriFormatException)
            {
                if (str.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) == false)
                {
                    str = "http://" + str;
                    return Convert(str, targetType, parameter, culture);
                }
            }
            return null;
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
                return Convert(values[0], targetType, parameter, culture);

            return null;
        }
    }
}
