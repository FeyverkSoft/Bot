using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Attributes;
using WpfConverters.Converters;

namespace WpfExecutor.Converter
{
    public class ActionListComboBox : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
           var t= value?.GetType();
          var name =  (t?.GetCustomAttribute<LocDescriptionAttribute>() ?? t?.GetCustomAttribute<DescriptionAttribute>())?
                .Description ?? t?.Name;
            return $"{name} : {t?.Assembly.GetName().Name}";
        }
    }
}
