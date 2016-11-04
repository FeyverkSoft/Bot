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
            List<IAction> actions = new List<IAction>();
            foreach (var type in list.Where(x => x.GetInterfaces().Contains(typeof(IAction)) && x.Name != nameof(BaseActionObject)))
            {
                var memberInfos = type.GetProperty(nameof(BaseActionObject.ActionType));
                if (memberInfos == null)
                    continue; ;
                if ((ActionType)memberInfos.GetValue(null, null) == actionType)
                {
                    var obj = type.GetConstructor(Type.EmptyTypes)?.Invoke(null);
                    if (obj != null)
                        actions.Add((IAction)obj);
                }
            }
            return actions;
        }

        public static ActionType GetType(IAction action)
        {
            switch (action.GetType().Name)
            {
                case nameof(MouseMoveAct):
                    return ActionType.MouseMove;
                case nameof(MouseSetPosAct):
                    return ActionType.MouseSetPos;
                case nameof(KeyBoardAct):
                    return ActionType.KeyBoard;
                case nameof(KeyBoardKeysAct):
                    return ActionType.KeyBoardKeys;
                case nameof(SleepAct):
                    return ActionType.Sleep;
                case nameof(LoopAct):
                    return ActionType.Loop;
                case nameof(PluginInvokeAct):
                    return ActionType.PluginInvoke;
                case nameof(IfAction):
                    return ActionType.If;
                case nameof(ExpectWindowAct):
                    return ActionType.ExpectWindow;
                case nameof(GetObjectAct):
                    return ActionType.GetObject;
                case nameof(ScreenShotAct):
                    return ActionType.GetScreenshot;
                case nameof(MockAction):
                    return ActionType.Mock;
                case nameof(SendMessageAct):
                    return ActionType.SendMessage;
                case nameof(LabelAct):
                    return ActionType.Label;
                case nameof(GoToAct):
                    return ActionType.GOTO;
                case nameof(StackAct):
                    return ActionType.Stack;
                default:
                    return ActionType.PluginAct;
            }
        }
    }
}
