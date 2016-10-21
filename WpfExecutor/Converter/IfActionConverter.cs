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
    public class IfActionConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var val = value as IfAction;
            if (val == null)
                throw new NotSupportedException();
            List<ListBotAction> list = new List<ListBotAction>
            {
                val.Actions,
                val.FailActions
            };
            return list;
        }
    }
}
