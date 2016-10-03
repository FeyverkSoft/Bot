using System.Collections.Generic;

namespace Core.ConfigEntity.ActionObjects
{
    public sealed class ListAct : List<IAction>
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
        public static implicit operator ListAct(List<KeyBoardAct> list)
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
    }
}
