using Core.ConfigEntity;
using Core.ActionExecutors;
using System.Collections.Generic;

namespace Core.Core
{
    /// <summary>
    /// Дефолтная фабрика действий
    /// </summary>
    internal sealed class DefaultActionFactory : IActionFactory
    {
        private readonly Dictionary<ActionType, IExecutor> _executorCache = new Dictionary<ActionType, IExecutor>();
        /// <summary>
        /// Возвращает исполнителя по типу действия
        /// </summary>
        /// <param name="type">Тип действия для которого происходит поиск фабрики</param>
        /// <returns></returns>
        public IExecutor GetExecutorAction(ActionType type)
        {
            //пока что обойдемся без нинжекта...
            switch (type)
            {
                case ActionType.MouseMove:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseMoveExecutor());
                    return _executorCache[type];
                case ActionType.MouseSetPos:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseSetPosExecutor());
                    return _executorCache[type];
                case ActionType.MouseRClick:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseRClickExecutor());
                    return _executorCache[type];
                case ActionType.MouseRPress:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseRPressExecutor());
                    return _executorCache[type];
                case ActionType.MouseRUp:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseRUpExecutor());
                    return _executorCache[type];
                case ActionType.MouseLPress:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseLPressExecutor());
                    return _executorCache[type];
                case ActionType.MouseLUp:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseLUpExecutor());
                    return _executorCache[type];
                case ActionType.MouseLClick:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new MouseLClickExecutor());
                    return _executorCache[type];
                case ActionType.KeyBoard:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new KeyBoardExecutor());
                    return _executorCache[type];
                case ActionType.KeyBoardKeys:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new KeyBoardKeysExecutor());
                    return _executorCache[type];
                case ActionType.Sleep:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new SleepExecutor());
                    return _executorCache[type];
                case ActionType.ExpectWindow:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new ExpectWindowExecutor());
                    return _executorCache[type];
                case ActionType.If:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new IfExecutor());
                    return _executorCache[type];
                case ActionType.GetObject:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new GetObjectExecutor());
                    return _executorCache[type];
                case ActionType.GetScreenshot:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new GetScreenShotExecutor());
                    return _executorCache[type];
                case ActionType.GetMousePos:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new GetMousePosExecutor());
                    return _executorCache[type];
                case ActionType.PluginInvoke:
                    if (!_executorCache.ContainsKey(type))
                        _executorCache.Add(type, new PluginInvokeExecutor());
                    return _executorCache[type];
                case ActionType.Loop:
                case ActionType.Mock:
                default:
                    return new MockExecutor();
            }
        }
    }
}
