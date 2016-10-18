using System;
using System.Globalization;

namespace WpfConverters.Converters.Common
{
    public class ValueNotInListConverter : ValueInListConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var result = base.Convert(values, targetType, parameter, culture) as bool?;
            return !result ?? false;
        }
    }
}
