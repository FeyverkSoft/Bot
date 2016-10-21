using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.Attributes;
using Core.ConfigEntity.ActionObjects;

namespace Core.Helpers
{
    public static class ActionHelper
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertysInfo = new Dictionary<Type, PropertyInfo[]>();
        public static String GetString(this IAction act)
        {
            if (act == null)
                return String.Empty;

            var sb = new StringBuilder();
            var actType = act.GetType();

            if (!PropertysInfo.ContainsKey(actType))
            {
                var _props = actType.GetProperties().Where(y => y.GetMethod.IsPublic)
                    .Where(x => !x.PropertyType.IsArray)
                    .Where(y => !y.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                    .ToArray();
                PropertysInfo.Add(actType, _props);
            }

            var props = PropertysInfo[actType];
            for (var i = 0; i < props.Length; i++)
            {
                var val = props[i].GetValue(act);
                if (val == null) continue;
                var dsAttr = props[i].GetCustomAttribute(typeof(LocDescriptionAttribute), false) as LocDescriptionAttribute;
                if (i + 1 != props.Length)//избавляемся от лишнего перевода строки
                    sb.AppendLine($"{dsAttr?.Description}: {val}");
                else
                    sb.Append($"{dsAttr?.Description}: {val}");
            }
            return sb.ToString();
        }
    }
}