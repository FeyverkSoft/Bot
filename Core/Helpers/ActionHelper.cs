using System;
using System.Collections;
using System.Linq;
using System.Text;
using Core.ConfigEntity.ActionObjects;

namespace Core.Helpers
{
    public static class ActionHelper
    {
        public static String GetString(this IAction act)
        {
            if (act == null)
                return String.Empty;

            var sb = new StringBuilder();
            var props = act.GetType().GetProperties().Where(y => y.GetMethod.IsPublic)
                .Where(x => !x.PropertyType.IsArray)
                .Where(y=>!y.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                .ToArray();
            for (var i = 0; i < props.Length; i++)
            {
                var val = props[i].GetValue(act);
                if (val == null) continue;

                if (i + 1 != props.Length)//избавляемся от лишнего перевода строки
                    sb.AppendLine(val.ToString());
                else
                    sb.Append(val);
            }
            return sb.ToString();
        }
    }
}