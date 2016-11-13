using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using CommonLib.Helpers;
using Core.Core;
using Core.Helpers;

namespace WpfExecutor.Model.ConditionalEditor
{
    public class ConditionalEditorViewModel : BaseViewModel
    {
        public static readonly Dictionary<Type, IEnumerable<PropertyInfo>> Props = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        Tuple<String, Type> _selectedItem;
        public Tuple<String, Type> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                Refresh(value.Item2);
            }
        }

        public List<Tuple<String, Type>> ExecutorResultList { get; } = new List<Tuple<string, Type>>();

        public ConditionalEditorViewModel()
        {
            foreach (var typ in ActionFactory.ExResultTypes)
            {
                ExecutorResultList.Add(new Tuple<String, Type>(typ.GetLocalName(), typ));
            }
        }


        private static readonly Type[] WriteTypes = {
        typeof(string), typeof(DateTime), typeof(Enum),
        typeof(decimal), typeof(Guid)
    };
        /// <summary>
        /// Это объект простого типа?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Boolean IsSimpleType(Type type)
        {
            return type.IsPrimitive || WriteTypes.Contains(type) || type.IsEnum || type.IsValueType;
        }

        private void GetPropList(Type item, ref List<Tuple<String, Type>> list, String path, Int32 deep)
        {
            if (deep > 8)
                return;
            var props = item.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in props)
            {
                if (IsSimpleType(propertyInfo.PropertyType))
                {
                    list.Add(new Tuple<String, Type>(
                        String.IsNullOrEmpty(path) ? propertyInfo.Name : $"{path}.{propertyInfo.Name}",
                        propertyInfo.PropertyType));
                }
                else
                {
                    GetPropList(propertyInfo.PropertyType, ref list, String.IsNullOrEmpty(path) ? propertyInfo.Name : $"{path}.{propertyInfo.Name}", deep + 1);
                }
            }
        }

        public void Refresh(Type item)
        {
            var refList = new List<Tuple<String, Type>>();
            GetPropList(item, ref refList, null, 0);
            refList = refList;

        }
    }
}
