using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Core.Attributes;
namespace WpfExecutor.Extensions.Provider
{
    public static class LocDescriptionProvider
    {
        private static readonly Dictionary<String, MemberInfo> AttrInfo = new Dictionary<String, MemberInfo>();

        /// <summary>
        /// Возвращает локализованный объект по привязки
        /// </summary>
        /// <param name="value">Ключ</param>
        /// <returns></returns>
        public static Object Localize(Object value)
        {
            var key = $"{value.GetType().FullName}_{value}";
            if (!AttrInfo.ContainsKey(key))
                AttrInfo.Add(key, value.GetType().GetMember(value.ToString()).FirstOrDefault());

            var attr = AttrInfo[key]?.GetCustomAttribute(typeof(LocDescriptionAttribute)) as LocDescriptionAttribute ?? AttrInfo[key]?.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attr?.Description;
        }
    }
}
