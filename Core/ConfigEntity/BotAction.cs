﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        private Boolean _isCurrent = false;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Указывает на то что в текущий момент выполняется это действие.
        /// </summary>
        public Boolean IsCurrent
        {
            get { return _isCurrent; }
            set
            {
                _isCurrent = value;
                OnPropertyChanged();
            }
        }

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
        [IgnoreDataMember]
        public Boolean IsValid
        {
            get
            {
                if (!IsMultiAct && SubActions.Count > 1)
                    return false;

                var flag = true; //пустое валидно!
                foreach (var subAction in SubActions)
                {
                    Log.WriteLine($"{GetType().Name}.IsValid->(ActionType:{ActionType}; subAction: {subAction.GetType().Name})");
                    flag = (ActionType)subAction.GetType().GetProperty(nameof(ActionType)).GetValue(null,null) == ActionType;
                }
                return flag;
            }
        }

        /// <summary>
        /// Указавает поддерживается ли множественные действия или нет
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember]
        public Boolean IsMultiAct
        {
            get
            {
                switch (this.ActionType)
                {
                    case ActionType.MouseMove:
                        return true;
                    case ActionType.MouseSetPos:
                        return true;
                    case ActionType.Run:
                        return false;
                    case ActionType.KeyBoardPressKey:
                    case ActionType.KeyBoardShortcut:
                    case ActionType.KeyBoardAction:
                        return true;
                    case ActionType.Sleep:
                        return false;
                    case ActionType.Loop:
                        return true;
                    case ActionType.ExpectWindow:
                        return true;
                    case ActionType.If:
                        return false;
                    case ActionType.GetObject:
                        return false;
                    case ActionType.PluginInvoke:
                        return true;
                    case ActionType.GetScreenshot:
                        return true;
                    case ActionType.SendMessage:
                        return false;
                    case ActionType.GOTO:
                        return false;
                    case ActionType.Label:
                        return false;
                    case ActionType.PluginAct:
                        return true;
                    case ActionType.Mock:
                        return true;
                    case ActionType.Stack:
                        return false;
                    default:
                        return true;
                }
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
