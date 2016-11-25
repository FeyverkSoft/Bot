using System.Collections.Generic;
using CommonLib.Collections;

namespace Core.ConfigEntity.ActionObjects
{
    public class ListAct : NotifyList<IAction>
    {
        public static implicit operator ListAct(List<MouseMoveAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<MouseSetPosAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<KeyBoardPressKeyAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<SleepAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<ExpectWindowAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<IfAction> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<ScreenShotAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<PluginInvokeAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<MockAction> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAct(List<SendMessageAct> list)
        {
            var temp = new ListAct();
            temp.AddRange(list);
            return temp;
        }
    }
}
