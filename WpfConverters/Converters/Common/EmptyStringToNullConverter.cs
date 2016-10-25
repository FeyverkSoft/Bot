using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Преобразует пустую строку в NULL
    /// </summary>
    public class EmptyStringToNullConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var s = value as String;
            if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
                return null;
            return s;
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
                return String.Empty;
            return value;
        }
    }
}
