using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Core.ConfigEntity.ActionObjects;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity
{
    /// <summary>
    /// Действие бота, который он должен сделать
    /// </summary>
    [DataContract]
    public sealed class BotAction : IBotAction
    {
        /// <summary>
        /// Тип события
        /// </summary>
        [DataMember]
        public ActionType ActionType { get; private set; }
        /// <summary>
        /// Описание действий для данного события
        /// </summary>
        [DataMember]
        public ListAct SubActions { get; private set; } = new ListAct();
        [JsonConstructor]
        public BotAction(ActionType actionType)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(actionType:{actionType};)");
            ActionType = actionType;
        }

        public BotAction(ActionType actionType, ListAct subActions) : this(actionType)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(subActions.Count:{subActions?.Count ?? -1};)");
            if (subActions == null)
                throw new ArgumentNullException(nameof(subActions));
            SubActions = subActions;
        }

        public BotAction(ActionType actionType, List<IAction> subActions) : this(actionType)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(subActions.Count:{subActions?.Count ?? -1};)");
            if (subActions == null)
                throw new ArgumentNullException(nameof(subActions));
            SubActions.AddRange(subActions);
        }

        public BotAction(ActionType actionType, IAction subAction) : this(actionType)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(subActions.Count:{subAction};)");
            if (subAction == null)
                throw new ArgumentNullException(nameof(subAction));
            SubActions.Add(subAction);
        }

        /// <summary>
        /// Провалидировать массив событий
        /// </summary>
        /// <returns></returns>
        public Boolean IsValid
        {
            get
            {
                var flag = true; //пустое валидно!
                foreach (var subAction in SubActions)
                {
                    Log.WriteLine($"{GetType().Name}.IsValid->(ActionType:{ActionType}; subAction: {subAction.GetType().Name})");
                    switch (ActionType)
                    {
                        case ActionType.MouseMove:
                            flag = subAction is MouseMoveAct;
                            break;
                        case ActionType.MouseSetPos:
                            flag = subAction is MouseSetPosAct;
                            break;
                        case ActionType.KeyBoard:
                        case ActionType.KeyBoardKeys:
                            flag = subAction is KeyBoardAct;
                            break;
                        case ActionType.Sleep:
                            flag = subAction is SleepAct;
                            break;
                        case ActionType.Loop:
                            flag = subAction is LoopAct;
                            break;
                        case ActionType.ExpectWindow:
                            flag = subAction is ExpectWindowAct;
                            break;
                        case ActionType.If:
                            flag = subAction is IfAction;
                            break;
                        case ActionType.GetObject:
                            flag = subAction is GetObjectAct;
                            break;
                        case ActionType.PluginInvoke:
                            flag = subAction is PluginInvokeAct;
                            break;
                        case ActionType.GetScreenshot:
                            flag = subAction is ScreenShotAct;
                            break;
                        case ActionType.SendMessage:
                            flag = subAction is SendMessageAct;
                            break;
                        case ActionType.PluginAct:
                            flag = true;
                            break;
                        default:
                            if (subAction is MockAction)
                                return true;
                            throw new Exception($"Incorrect ActionType ({ActionType})");
                    }
                }
                return flag;
            }
        }

        /// <summary>
        /// Возвращает строку, представляющую текущий объект.
        /// </summary>
        /// <returns>
        /// Строка, представляющая текущий объект.
        /// </returns>
        public override String ToString()
        {
            return $"{ActionType}::\t{base.ToString()}";
        }
    }
}
