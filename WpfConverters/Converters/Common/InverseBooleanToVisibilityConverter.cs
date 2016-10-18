using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Инвертирует переданное значение типа <see cref="bool"/> и преобразует результат в значение типа <see cref="Visibility"/>
    /// </summary>
    public class InverseBooleanToVisibilityConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            if (targetType != typeof(Visibility) && targetType != typeof(Visibility?) && targetType != typeof(object))
                throw new ArgumentException($"{nameof(System.Convert)}: Incorrect type for targetType: {targetType.Name}");

            var bValue = value as bool? ?? false;
            if (value is bool)
                bValue = (bool)value;
            return bValue ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            if (targetType != typeof(bool) && targetType != typeof(bool?) && targetType != typeof(object))
                throw new ArgumentException($"{nameof(System.Convert)}: Incorrect type for targetType: {targetType.Name}");

            if (value is Visibility)
                return (Visibility)value != Visibility.Visible;
            return false;
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = values.FirstOrDefault();
            return Convert(value, targetType, parameter, culture);
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var result = ConvertBack(value, targetTypes.FirstOrDefault(), parameter, culture);
            return new[] { result };
        }
    }
}
