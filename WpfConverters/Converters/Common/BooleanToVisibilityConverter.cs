using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace WpfConverters.Converters.Common
{
    public class BooleanToVisibilityConverter : BaseHybridConverterExtension
    {
        /// <summary>
        /// Convert bool or Nullable&lt;bool&gt; to Visibility
        /// </summary>
        /// <param name="value">bool or Nullable&lt;bool&gt;</param>
        /// <param name="targetType">Visibility</param>
        /// <param name="parameter">null</param>
        /// <param name="culture">null</param>
        /// <returns>Visible or Collapsed</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            var type = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if (type != typeof(Visibility) && targetType != typeof(object))
                throw new ArgumentException($"{nameof(System.Convert)}: Incorrect type for targetType: {targetType.Name}");

            var bValue = value as bool? ?? false;
            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Convert Visibility to boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            return value as Visibility? == Visibility.Visible;
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
