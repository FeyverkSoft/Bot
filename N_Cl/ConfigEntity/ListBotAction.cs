using System.Collections.Generic;

namespace Executor.ConfigEntity
{
    /// <summary>
    /// Список действий бота
    /// </summary>
    public sealed class ListBotAction : List<IBotAction>
    {
        public static implicit operator ListBotAction(List<BotAction> list)
        {
            var temp = new ListBotAction();
            temp.AddRange(list);
            return temp;
        }
    }
}
