using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Input;
using CommonLib.Attributes;
using CommonLib.Collections;
using CommonLib.Helpers;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;
using WpfConverters.Extensions.Commands;
using WpfExecutor.Model.Add;

namespace WpfExecutor.Model.ConditionalEditor
{
    public class ConditionalEditorViewModel : BaseViewModel
    {
        public static readonly Dictionary<Type, IEnumerable<PropertyInfo>> Props = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        Tuple<String, Type> _selectedItem;
        private NotifyList<ConditionalParamModel> _conditionalsList = new NotifyList<ConditionalParamModel>();
        private List<Tuple<String, Type>> _paramsList;

        public Tuple<String, Type> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                Refresh(value.Item2);
            }
        }

        public Tuple<String, Type> SelectedProp { get; set; }

        public NotifyList<ConditionalParamModel> ConditionalsList
        {
            get { return _conditionalsList; }
            set
            {
                _conditionalsList = value;
                OnPropertyChanged();
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

        public List<Tuple<String, Type>> ParamsList
        {
            get { return _paramsList; }
            set
            {
                _paramsList = value;
                OnPropertyChanged();
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

        private void GetPropList(Type item, ref NotifyList<Tuple<String, Type>> list, String path, Int32 deep)
        {
            if (deep > 8)
                return;
            var props = item.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in props)
            {
                if (IsSimpleType(propertyInfo.PropertyType) || propertyInfo.GetCustomAttribute<VisualCtorIgnoreChildProp>() != null)
                {
                    list.Add(new Tuple<String, Type>
                        (
                        String.IsNullOrEmpty(path) ? propertyInfo.Name : $"{path}.{propertyInfo.Name}",
                          propertyInfo.PropertyType
                        ));
                }
                else
                {
                    GetPropList(propertyInfo.PropertyType, ref list, String.IsNullOrEmpty(path) ? propertyInfo.Name : $"{path}.{propertyInfo.Name}", deep + 1);
                }
            }
        }

        public void Refresh(Type item)
        {
            var refList = new NotifyList<Tuple<String, Type>>();
            GetPropList(item, ref refList, null, 0);
            ParamsList = refList;
            ConditionalsList = new NotifyList<ConditionalParamModel>();
        }

        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandMethod));

        private void AddCommandMethod()
        {
            if (SelectedProp != null)
            {
                ConditionalsList.Add(new ConditionalParamModel
                {
                    Name = SelectedProp.Item1,
                    ValueType = SelectedProp.Item2
                });
            }
        }
    }
}
