using System;
using System.Collections.Generic;
using System.Linq;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;

namespace Core.Core
{
    public static class ActionFactory
    {
        private static List<Type> _list1;

        private static List<Type> List
        {
            get
            {
                if (_list1 != null)
                    return _list1;
                _list1 = Assemblys.TypeList.Where(x => x.GetInterfaces().Contains(typeof(IAction))).ToList();
                _list1.AddRange(Assemblys.PluginsTypeList.Where(x => x.GetInterfaces().Contains(typeof(IAction))));
                _list1 =_list1.Distinct().ToList();
                return _list1;
            }
        }

        public static List<IAction> Get(ActionType actionType)
        {
            var actions = new List<IAction>();
            foreach (var type in List.Where(x => x.Name != nameof(BaseActionObject)))
            {
                var propertyInfo = type.GetProperty(nameof(BaseActionObject.ActionType));
                if (propertyInfo == null)
                    continue;

                if ((ActionType) propertyInfo.GetValue(null, null) == actionType)
                {
                    var obj = type.GetConstructor(Type.EmptyTypes)?.Invoke(null);
                    if (obj != null)
                        actions.Add((IAction) obj);
                }
            }
            return actions;
        }

        public static ActionType GetType(IAction action)
        {
            return (ActionType)action.GetType().GetProperty(nameof(BaseActionObject.ActionType)).GetValue(null, null);
        }
    }
}
