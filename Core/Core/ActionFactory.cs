using System;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;

namespace Core.Core
{
    public static class ActionFactory
    {
        public static IAction Get(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.MouseMove:
                    return new MouseMoveAct();
                case ActionType.MouseSetPos:
                    return new MouseSetPosAct();
                case ActionType.MouseRClick:
                case ActionType.MouseRPress:
                case ActionType.MouseRUp:
                case ActionType.MouseLPress:
                case ActionType.MouseLUp:
                case ActionType.MouseLClick:
                case ActionType.GetMousePos:
                    return null;
                case ActionType.KeyBoard:
                    return new KeyBoardKeysAct();
                case ActionType.KeyBoardKeys:
                    return new KeyBoardAct();
                case ActionType.Sleep:
                    return new SleepAct();
                case ActionType.Loop:
                    return new LoopAct();
                case ActionType.PluginInvoke:
                    return new PluginInvokeAct();
                case ActionType.If:
                    return new IfAction();
                case ActionType.ExpectWindow:
                    return new ExpectWindowAct();
                case ActionType.GetObject:
                    return new GetObjectAct();
                case ActionType.GetScreenshot:
                    return new ScreenShotAct();
                case ActionType.Mock:
                    return new MockAction();
                case ActionType.PluginAct:
                    return null;
                case ActionType.SendMessage:
                    return new SendMessageAct();
                case ActionType.Label:
                    return new LabelAct();
                case ActionType.GOTO:
                    return new GoToAct();
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
