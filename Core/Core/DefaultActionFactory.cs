using Core.ConfigEntity;
using Core.ActionExecutors;
using System.Collections.Generic;

namespace Core.Core
{
    /// <summary>
    /// Дефолтная фабрика действий
    /// </summary>
    public sealed class DefaultActionFactory : IActionFactory
    {
        private Dictionary<ActionType, IExecutor> executorCache = new Dictionary<ActionType, IExecutor>();
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
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new MouseMoveExecutor());
                    return executorCache[type];
                case ActionType.MouseSetPos:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new MouseSetPosExecutor());
                    return executorCache[type];
                case ActionType.MouseRClick:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new MouseRClickExecutor());
                    return executorCache[type];
                case ActionType.MouseLClick:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new MouseLClickExecutor());
                    return executorCache[type];
                case ActionType.KeyBoard:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new KeyBoardExecutor());
                    return executorCache[type];
                case ActionType.KeyBoardKeys:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new KeyBoardKeysExecutor());
                    return executorCache[type];
                case ActionType.Sleep:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new SleepExecutor());
                    return executorCache[type];
                case ActionType.Ai:
                    return new MockExecutor();
                case ActionType.PluginInvoke:
                    return new MockExecutor();
                case ActionType.ExpectWindow:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new ExpectWindowExecutor());
                    return executorCache[type];
                case ActionType.If:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new IfExecutor());
                    return executorCache[type];
                case ActionType.GetObject:
                    if (!executorCache.ContainsKey(type))
                        executorCache.Add(type, new GetObjectExecutor());
                    return executorCache[type];
                case ActionType.Loop:
                case ActionType.GetScreenshot:
                case ActionType.Mock:
                default:
                    return new MockExecutor();
            }
        }
    }
}
