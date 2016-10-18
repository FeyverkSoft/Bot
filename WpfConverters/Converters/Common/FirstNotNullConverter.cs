using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace WpfConverters.Converters.Common
{
    /// <summary>
    /// Возвращает первое значение, которое не равно null и не равно указанному пераметру
    /// </summary>
    public class FirstNotNullConverter : BaseHybridConverterExtension
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values?.Length == 0)
                throw new ArgumentException(nameof(values));

            return values?.FirstOrDefault(x => x != null && x != parameter && x != DependencyProperty.UnsetValue);
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return parameter;

            return value;
        }
    }
}
