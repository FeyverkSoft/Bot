using System;
using System.Globalization;
using Core.Core;
using WpfConverters.Converters;

namespace WpfExecutor.Converter
{
    public class PointConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = ((Point)value);
            if (parameter != null && parameter.ToString().ToUpper() == "X")
            {
                return t.X;
            }
            if (parameter != null && parameter.ToString().ToUpper() == "Y")
            {
                return t.Y;
            }
            throw new NotSupportedException();
        }
    }
}
