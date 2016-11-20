using System;
using System.Globalization;
using WpfConverters.Converters;
using CommonLib.Helpers;

namespace WpfExecutor.Converter
{
    public class ActionListComboBox : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
           var t= value?.GetType();
            return $"{t.GetLocalName()} : {t?.Assembly.GetName().Name}";
        }
    }
}
