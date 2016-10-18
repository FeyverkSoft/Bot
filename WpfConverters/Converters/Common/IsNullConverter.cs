using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Определяет является ли значение null
    /// </summary>
    public class IsNullConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || value == DBNull.Value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value as bool? ?? false;
            if (b)
                return null;

            var type = Nullable.GetUnderlyingType(targetType) ?? targetType;
            return Activator.CreateInstance(type);
        }
    }
}
