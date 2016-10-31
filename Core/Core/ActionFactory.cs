using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    return  null;
                case ActionType.KeyBoard:
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
    }
}
