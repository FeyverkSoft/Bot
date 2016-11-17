using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using CommonLib.Collections;
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

        public List<Tuple<String, Type>> ExecutorResultList { get; private set; } = new List<Tuple<string, Type>>();

        public ConditionalEditorViewModel()
        {
            foreach (var typ in ActionFactory.ExResultTypes)
            {
                ExecutorResultList.Add(new Tuple<String, Type>(typ.GetLocalName(), typ));
            }
        }


        public NotifyList<ConditionalModel> ConditionalsList = new NotifyList<ConditionalModel>();

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

        private void GetPropList(Type item, ref List<ConditionalModel> list, String path, Int32 deep)
        {
            if (deep > 8)
                return;
            var props = item.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in props)
            {
                if (IsSimpleType(propertyInfo.PropertyType))
                {
                    list.Add(
                        new ConditionalModel
                        {
                            Name = String.IsNullOrEmpty(path) ? propertyInfo.Name : $"{path}.{propertyInfo.Name}",
                            Type = propertyInfo.PropertyType,

                        });
                }
                else
                {
                    GetPropList(propertyInfo.PropertyType, ref list, String.IsNullOrEmpty(path) ? propertyInfo.Name : $"{path}.{propertyInfo.Name}", deep + 1);
                }
            }
        }

        public void Refresh(Type item)
        {
            var refList = new List<ConditionalModel>();
            GetPropList(item, ref refList, null, 0);
            ConditionalsList = (NotifyList<ConditionalModel>)refList;
        }
    }


    public class ConditionalModel
    {
        /// <summary>
        /// Название поля
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Тип поля
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Значение для сравнения
        /// </summary>
        public Object ConditionalValue { get; set; }
        /// <summary>
        /// Условный оператор, ==, !=, итд
        /// </summary>
        public Conditional Conditional { get; set; }
    }

    public enum Conditional
    {

    }
}
