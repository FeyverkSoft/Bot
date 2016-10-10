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
        public ListBotAction Actions { get; set; } = new ListBotAction();

        public PluginInvokeAct(String pluginName, ListBotAction actions)
        {
            PluginName = pluginName;
            Actions = actions;
        }
    }
}
