using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Core.Attributes;
using Core.ConfigEntity;
using WpfConverters.Converters;

namespace WpfExecutor.Converter
{
    public class DevConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            try
            {
                var val = System.Convert.ToDouble(value);
                var param = System.Convert.ToDouble(parameter, NumberFormatInfo.InvariantInfo);

                return (val / param);
            }
            catch
            {
            }

            return value;
        }
    }
}
