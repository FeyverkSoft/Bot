using System;
using System.Runtime.Serialization;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Информация для вызова плагина
    /// </summary>
    public class PluginInvokeAct : IAction
    {
        /// <summary>
        /// Наименование плагина
        /// </summary>
        public String PluginName { get; private set; }

        /// <summary>
        /// Список действий которые необходимо передать плагину
        /// </summary>
        [DataMember]
        public BotAction Actions { get; set; }

        public PluginInvokeAct(String pluginName, BotAction actions)
        {
            PluginName = pluginName;
            Actions = actions;
        }
    }
}
