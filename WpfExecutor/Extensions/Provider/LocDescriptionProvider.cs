using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonLib.Helpers;

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

            return AttrInfo[key]?.GetLocalName();
        }
    }
}
