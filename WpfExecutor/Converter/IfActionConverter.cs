using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using WpfConverters.Converters;

namespace WpfExecutor.Converter
{
    public class IfActionConverter : BaseMultiValueConverterExtension
    {
        public override Object Convert(Object[] value, Type targetType, Object parameter, CultureInfo culture)
        {
            var list = new List<Object>
            {
               new IfList {
                   ListName = "Actions",
                   List =value[0] as ListBotAction},
               new IfList {
                   ListName = "FailActions",
                   List=  value[1] as ListBotAction}
            };
            return list;
        }
    }

    public class IfList
    {
        public String ListName { get; set; }
        public ListBotAction List { get; set; }
    }
}
