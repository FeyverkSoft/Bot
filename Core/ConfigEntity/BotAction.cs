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
        public ListAction SubActions { get; private set; } = new ListAction();
        [JsonConstructor]
        public BotAction(ActionType actionType)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(actionType:{actionType};)");
            ActionType = actionType;
        }

        public BotAction(ActionType actionType, ListAction subActions) : this(actionType)
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
                        default:
                            throw new Exception($"Incorrect ActionType ({ActionType})");
                    }
                }
                return flag;
            }
        }

    }
}
