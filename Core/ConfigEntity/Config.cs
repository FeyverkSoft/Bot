using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;
using Core.ConfigEntity.ActionObjects;
using LogWrapper;

namespace Core.ConfigEntity
{
    [DataContract]
    public sealed class Config : IBotActionContainer
    {
        /// <summary>
        /// Версия бота для этого конфига
        /// </summary>
        [DataMember]
        public FileVersion BotVer { get; private set; } = Assembly.GetExecutingAssembly().GetName().Version;
        /// <summary>
        /// Список действий
        /// </summary>
        [DataMember]
        public ListBotAction Actions { get; set; } = new ListBotAction();

        public Config()
        {
            Log.WriteLine($"{GetType().Name}.ctor->()");
            BotVer = new FileVersion(Assembly.GetExecutingAssembly().GetName().Version);
        }

        public Config(ListBotAction actions, FileVersion botVer = null)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(actions:{actions?.Count ?? -1}; botVer: {botVer?.ToString() ?? "null"})");
            if (botVer != null)
                BotVer = botVer;

            if (actions != null)
                Actions = actions;
        }

        public Config(String botVer, ListBotAction actions = null) : this(actions, new FileVersion(botVer))
        {
        }

    }
}
