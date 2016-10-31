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
            if (!_executorCache.ContainsKey(type))
                switch (type)
                {
                    case ActionType.MouseMove:
                        _executorCache.Add(type, new MouseMoveExecutor());
                        break;
                    case ActionType.MouseSetPos:
                        _executorCache.Add(type, new MouseSetPosExecutor());
                        break;
                    case ActionType.MouseRClick:
                        _executorCache.Add(type, new MouseRClickExecutor());
                        break;
                    case ActionType.MouseRPress:
                        _executorCache.Add(type, new MouseRPressExecutor());
                        break;
                    case ActionType.MouseRUp:
                        _executorCache.Add(type, new MouseRUpExecutor());
                        break;
                    case ActionType.MouseLPress:
                        _executorCache.Add(type, new MouseLPressExecutor());
                        break;
                    case ActionType.MouseLUp:
                        _executorCache.Add(type, new MouseLUpExecutor());
                        break;
                    case ActionType.MouseLClick:
                        _executorCache.Add(type, new MouseLClickExecutor());
                        break;
                    case ActionType.KeyBoard:
                        _executorCache.Add(type, new KeyBoardExecutor());
                        break;
                    case ActionType.KeyBoardKeys:
                        _executorCache.Add(type, new KeyBoardKeysExecutor());
                        break;
                    case ActionType.Sleep:
                        _executorCache.Add(type, new SleepExecutor());
                        break;
                    case ActionType.ExpectWindow:
                        _executorCache.Add(type, new ExpectWindowExecutor());
                        break;
                    case ActionType.If:
                        _executorCache.Add(type, new IfExecutor());
                        break;
                    case ActionType.GetObject:
                        _executorCache.Add(type, new GetObjectExecutor());
                        break;
                    case ActionType.GetScreenshot:
                        _executorCache.Add(type, new GetScreenShotExecutor());
                        break;
                    case ActionType.GetMousePos:
                        _executorCache.Add(type, new GetMousePosExecutor());
                        break;
                    case ActionType.PluginInvoke:
                        _executorCache.Add(type, new PluginInvokeExecutor());
                        break;
                    case ActionType.Loop:
                        _executorCache.Add(type, new MockExecutor());
                        break;
                    case ActionType.Mock:
                        _executorCache.Add(type, new MockExecutor());
                        break;
                    case ActionType.PluginAct:
                        _executorCache.Add(type, new MockExecutor());
                        break;
                    case ActionType.SendMessage:
                        _executorCache.Add(type, new SendMessageExecutor());
                        break;
                    default:
                        return new MockExecutor();
                }
            return _executorCache[type];
        }
    }
}
