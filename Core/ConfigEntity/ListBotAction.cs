using System.Collections.Generic;
using CommonLib.Collections;

namespace Core.ConfigEntity
{
    /// <summary>
    /// Список действий бота
    /// </summary>
    public sealed class ListBotAction : NotifyList<IBotAction>
    {
        public static implicit operator ListBotAction(List<BotAction> list)
        {
            var temp = new ListBotAction();
            temp.AddRange(list);
            return temp;
        }
        public static implicit operator ListBotAction(NotifyList<BotAction> list)
        {
            var temp = new ListBotAction();
            temp.AddRange(list);
            return temp;
        }
    }
}
