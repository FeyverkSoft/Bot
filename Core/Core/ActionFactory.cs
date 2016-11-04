using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;

namespace Core.Core
{
    public static class ActionFactory
    {
        public static List<IAction> Get(ActionType actionType)
        {
            var list = Assemblys.TypeList;
            list.AddRange(Assemblys.PluginsTypeList);
            var actions = new List<IAction>();
            foreach (var type in list.Where(x => x.GetInterfaces()
            .Contains(typeof(IAction)) && x.Name != nameof(BaseActionObject)))
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
