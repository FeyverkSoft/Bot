using System.Collections.Generic;

namespace Executor.ConfigEntity.ActionObjects
{
    public sealed class ListAction : List<IAction>
    {
        public static implicit operator ListAction(List<MouseMoveAct> list)
        {
            var temp = new ListAction();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAction(List<MouseSetPosAct> list)
        {
            var temp = new ListAction();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAction(List<KeyBoardAct> list)
        {
            var temp = new ListAction();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAction(List<SleepAct> list)
        {
            var temp = new ListAction();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListAction(List<ExpectWindowAct> list)
        {
            var temp = new ListAction();
            temp.AddRange(list);
            return temp;
        }
    }
}
