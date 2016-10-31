using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Core.Attributes;
using Core.ConfigEntity;
using Core.Core;
using WpfConverters.Extensions.Commands;

namespace WpfExecutor.Model.Add
{
    /// <summary>
    /// Настройка и генерация элементов типа IAction
    /// </summary>
    public sealed class AddActionViewModel : BaseViewModel
    {
        /// <summary>
        /// Кешируем информацию о свойствах объекта
        /// </summary>
        private static readonly Dictionary<Type, List<PropertyInfo>> Props = new Dictionary<Type, List<PropertyInfo>>();

        private Tuple<String, Object> _currentType;
        private List<PropModel> _propsList;

        /// <summary>
        /// Текущий выбранный тип действия
        /// </summary>
        public Tuple<String, Object> CurrentType
        {
            get { return _currentType; }
            set
            {
                _currentType = value;
                Refresh();
            }
        }

        public List<PropModel> PropsList
        {
            get { return _propsList ?? (_propsList = new List<PropModel>()); }
            set
            {
                _propsList = value;
                OnPropertyChanged();
            }
        }

        private void Refresh()
        {
            var currentAct = ActionFactory.Get((ActionType)CurrentType.Item2);
            if (currentAct != null)
            {
                var type = currentAct.GetType();
                if (!Props.ContainsKey(type))
                {
                    Props.Add(type,
                        currentAct.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                            .Where(x => x.MemberType == MemberTypes.Property).Cast<PropertyInfo>()
                            .Where(
                                x =>
                                    !x.PropertyType.IsArray &&
                                    !x.PropertyType.GetInterfaces().Contains(typeof(ICollection))).ToList());
                }

                PropsList = Props[type].Select(x => new PropModel
                {
                    PropName = x.Name,
                    Name = x.GetCustomAttribute<LocDescriptionAttribute>()?.Description ?? x.Name,
                    Value = (x).GetValue(currentAct),
                    TypeName = x.PropertyType.Name
                }).ToList();
            }
            else
            {
                PropsList = new List<PropModel>();
            }
        }

        /// <summary>
        /// Комманда добавить
        /// </summary>
        private ICommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandMethod));
        /// <summary>
        /// Реализация комманды добавить
        /// </summary>
        private void AddCommandMethod()
        {

        }
    }
}
