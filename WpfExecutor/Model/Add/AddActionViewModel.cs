using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Core.Attributes;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
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

        private List<PropModel> _propsList;
        public List<PropModel> PropsList
        {
            get { return _propsList ?? (_propsList = new List<PropModel>()); }
            set
            {
                _propsList = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsSupported
        {
            get { return _isSupported; }
            set
            {
                _isSupported = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текущий выбранный тип действия
        /// </summary>
        private ActionType CurrentActionType { get; set; }
        /// <summary>
        /// Параметры действия
        /// </summary>
        public IAction Action{ get; private set; }

    public AddActionViewModel(ActionType actionType)
        {
            IsSupported = true;
            CurrentActionType = actionType;
            Refresh();
        }

        private void Refresh()
        {
            Action = ActionFactory.Get(CurrentActionType);
            if (Action != null)
            {
                IsSupported = true;
                var type = Action.GetType();
                if (!Props.ContainsKey(type))
                {
                    Props.Add(type,
                        Action.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
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
                    Value = x.GetValue(Action),
                    TypeName = x.PropertyType.Name
                }).ToList();
            }
            else
            {
                IsSupported = false;
                PropsList = new List<PropModel>();
            }
        }

        /// <summary>
        /// Комманда добавить
        /// </summary>
        private ICommand _addCommand;

        private Boolean _isSupported;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandMethod));
        /// <summary>
        /// Реализация комманды добавить
        /// </summary>
        private void AddCommandMethod()
        {
            foreach (var propModel in PropsList)
            {
                var prop = Props[Action.GetType()].First(x => x.Name == propModel.PropName);
                prop.SetValue(Action, propModel.Value);
            }
            CloseCommand?.Execute(true);
        }
    }
}
