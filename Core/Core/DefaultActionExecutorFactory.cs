using System;
using Core.ConfigEntity;
using Core.ActionExecutors;
using System.Collections.Generic;
using System.Linq;

namespace Core.Core
{
    /// <summary>
    /// Дефолтная фабрика действий
    /// </summary>
    internal sealed class DefaultActionExecutorFactory : IActionExecutorFactory
    {
        private readonly Dictionary<ActionType, List<Tuple<String, IExecutor>>> _executorCache = new Dictionary<ActionType, List<Tuple<String, IExecutor>>>();

        /// <summary>
        /// Возвращает исполнителя по типу действия
        /// </summary>
        /// <param name="actionType">Тип действия для которого происходит поиск фабрики</param>
        /// <returns></returns>
        public IExecutor GetExecutorAction(ActionType actionType)
        {
            if (!_executorCache.ContainsKey(actionType))
            {
                var list = Assemblys.TypeList;
                list.AddRange(Assemblys.PluginsTypeList);
                foreach (var type in list.Where(x => x.GetInterfaces().Contains(typeof(IExecutor)) && x.Name != nameof(BaseExecutor)))
                {
                    var propertyInfo = type.GetProperty(nameof(BaseExecutor.ActionType));
                    if (propertyInfo == null)
                        continue;

                    if ((ActionType)propertyInfo.GetValue(null, null) == actionType)
                    {
                        var obj = type.GetConstructor(Type.EmptyTypes)?.Invoke(null);
                        if (obj == null) continue;
                        if (!_executorCache.ContainsKey(actionType))
                        {
                            _executorCache.Add(actionType, new List<Tuple<String, IExecutor>>
                            {
                                new Tuple<String, IExecutor>(type.Assembly.GetName().Name, (IExecutor) obj)
                            });
                        }
                        else
                        {
                            _executorCache[actionType].Add(
                                new Tuple<String, IExecutor>(type.Assembly.GetName().Name, (IExecutor)obj)
                                );
                        }
                    }
                }
            }

            if (!_executorCache.ContainsKey(actionType))
                return null;
            //приоритет поиска
            foreach (var s in AppConfig.Instance.PrioritetList)
            {
                foreach (var item in _executorCache[actionType])
                {
                    if (item.Item1.Equals(s, StringComparison.InvariantCultureIgnoreCase))
                        return item.Item2;
                }
            }
            return null;
        }
    }
}
