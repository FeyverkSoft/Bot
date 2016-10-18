using System;
using System.Globalization;
using System.Windows;

namespace WpfConverters.Converters.Common
{
    public class TrimmedTextVisibilityConverter : BaseValueConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            if (targetType != typeof(Visibility) && targetType != typeof(Visibility?) && targetType != typeof(object))
                throw new ArgumentException($"{nameof(Convert)}: Incorrect type for targetType: {targetType.Name}");

            var element = value as FrameworkElement;
            if (element == null)
                return Visibility.Collapsed;

            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            if (element.ActualWidth + element.Margin.Left + element.Margin.Right < element.DesiredSize.Width)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
}
