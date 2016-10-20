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
        private readonly Dictionary<ActionType, IExecutor> ExecutorCache = new Dictionary<ActionType, IExecutor>();
        /// <summary>
        /// Возвращает исполнителя по типу действия
        /// </summary>
        /// <param name="type">Тип действия для которого происходит поиск фабрики</param>
        /// <returns></returns>
        public IExecutor GetExecutorAction(ActionType type)
        {
            //пока что обойдемся без нинжекта...
            if (!ExecutorCache.ContainsKey(type))
                switch (type)
                {
                    case ActionType.MouseMove:
                        ExecutorCache.Add(type, new MouseMoveExecutor());
                        break;
                    case ActionType.MouseSetPos:
                        ExecutorCache.Add(type, new MouseSetPosExecutor());
                        break;
                    case ActionType.MouseRClick:
                        ExecutorCache.Add(type, new MouseRClickExecutor());
                        break;
                    case ActionType.MouseRPress:
                        ExecutorCache.Add(type, new MouseRPressExecutor());
                        break;
                    case ActionType.MouseRUp:
                        ExecutorCache.Add(type, new MouseRUpExecutor());
                        break;
                    case ActionType.MouseLPress:
                        ExecutorCache.Add(type, new MouseLPressExecutor());
                        break;
                    case ActionType.MouseLUp:
                        ExecutorCache.Add(type, new MouseLUpExecutor());
                        break;
                    case ActionType.MouseLClick:
                        ExecutorCache.Add(type, new MouseLClickExecutor());
                        break;
                    case ActionType.KeyBoard:
                        ExecutorCache.Add(type, new KeyBoardExecutor());
                        break;
                    case ActionType.KeyBoardKeys:
                        ExecutorCache.Add(type, new KeyBoardKeysExecutor());
                        break;
                    case ActionType.Sleep:
                        ExecutorCache.Add(type, new SleepExecutor());
                        break;
                    case ActionType.ExpectWindow:
                        ExecutorCache.Add(type, new ExpectWindowExecutor());
                        break;
                    case ActionType.If:
                        ExecutorCache.Add(type, new IfExecutor());
                        break;
                    case ActionType.GetObject:
                        ExecutorCache.Add(type, new GetObjectExecutor());
                        break;
                    case ActionType.GetScreenshot:
                        ExecutorCache.Add(type, new GetScreenShotExecutor());
                        break;
                    case ActionType.GetMousePos:
                        ExecutorCache.Add(type, new GetMousePosExecutor());
                        break;
                    case ActionType.PluginInvoke:
                        ExecutorCache.Add(type, new PluginInvokeExecutor());
                        break;
                    case ActionType.Loop:
                        ExecutorCache.Add(type, new MockExecutor());
                        break;
                    case ActionType.Mock:
                        ExecutorCache.Add(type, new MockExecutor());
                        break;
                    case ActionType.PluginAct:
                        ExecutorCache.Add(type, new MockExecutor());
                        break;
                    case ActionType.SendMessage:
                        ExecutorCache.Add(type, new SendMessageExecutor());
                        break;
                    default:
                        return new MockExecutor();
                }
            return ExecutorCache[type];
        }
    }
}
