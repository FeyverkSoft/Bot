using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfConverters.Converters.Common
{
    public class MultilineOrTrimmedTextVisibilityConverter : BaseHybridConverterExtension
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null)
                throw new ArgumentNullException(nameof(targetType));

            if (targetType != typeof(Visibility) && targetType != typeof(Visibility?) && targetType != typeof(object))
                throw new ArgumentException($"{nameof(System.Convert)}: Incorrect type for targetType: {targetType.Name}");

            var element = value as TextBlock;
            if (element == null)
                return Visibility.Collapsed;

            if (element.Text?.Contains(Environment.NewLine) == true)
                return Visibility.Visible;

            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            if (element.ActualWidth + element.Margin.Left + element.Margin.Right < element.DesiredSize.Width)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0)
                return null;

            var value = values[0];
            return Convert(value, targetType, parameter, culture);
        }
    }
}
