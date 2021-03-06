﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CommonLib.Helpers;
using Core.ConfigEntity;

namespace Core.Helpers
{
    public static class ActionHelper
    {
        private static readonly Dictionary<Type, PropertyInfo[]> PropertysInfo = new Dictionary<Type, PropertyInfo[]>();
        private static readonly Dictionary<String, MemberInfo> AttrInfo = new Dictionary<String, MemberInfo>();
        public static String GetString(this Object act)
        {
            if (act == null)
                return String.Empty;

            var sb = new StringBuilder();
            var actType = act.GetType();

            if (!PropertysInfo.ContainsKey(actType))
            {
                var _props = actType.GetProperties().Where(y => y.GetMethod.IsPublic && !y.GetMethod.IsStatic)
                    .Where(x => !x.PropertyType.IsArray && x.Name != "ToString")
                    .Where(y => !y.PropertyType.GetInterfaces().Contains(typeof(ICollection)) &&
                    !y.PropertyType.GetInterfaces().Contains(typeof(IBotAction)))
                    .ToArray();
                PropertysInfo.Add(actType, _props);
            }

            var props = PropertysInfo[actType];
            for (var i = 0; i < props.Length; i++)
            {
                var val = props[i].GetValue(act);
                if (val == null) continue;
                var name = props[i].GetLocalName();
                if (i + 1 != props.Length)//избавляемся от лишнего перевода строки
                    sb.AppendLine($"{name}: {val.Localize()}");
                else
                    sb.Append($"{name}: {val.Localize()}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Возвращает локализованный объект по привязки
        /// </summary>
        /// <param name="value">Ключ</param>
        /// <returns></returns>
        public static String Localize(this Object value)
        {
            var key = $"{value.GetType().FullName}_{value}";
            if (!AttrInfo.ContainsKey(key))
                AttrInfo.Add(key, value.GetType().GetMember(value.ToString()).FirstOrDefault());

            return AttrInfo[key]?.GetLocalName() ?? value.ToString();
        }
    }
}