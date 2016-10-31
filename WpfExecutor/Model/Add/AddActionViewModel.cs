using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Attributes;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Core;

namespace WpfExecutor.Model.Add
{
    /// <summary>
    /// Настройка и генерация элементов типа IAction
    /// </summary>
    public sealed class AddActionViewModel : BaseViewModel
    {
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
            var currentAct = ActionFactory.Get((ActionType) CurrentType.Item2);
            if (currentAct != null)
            {
                PropsList = currentAct.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                   .Where(x => x.MemberType == MemberTypes.Property).Cast<PropertyInfo>()
                   .Where(x => !x.PropertyType.IsArray && !x.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                   .Select(x => new PropModel
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
    }
}
