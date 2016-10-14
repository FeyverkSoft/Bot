using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Информация для вызова плагина
    /// </summary>
    [Description("Информация для вызова плагина")]
    public class PluginInvokeAct : IAction
    {
        /// <summary>
        /// Наименование плагина
        /// </summary>
        [Description("Наименование плагина")]
        public String PluginName { get; private set; }

        /// <summary>
        /// Список действий которые необходимо передать плагину
        /// </summary>
        [DataMember]
        [Description("Список действий которые необходимо передать плагину")]
        public BotAction Actions { get; set; }

        public PluginInvokeAct(String pluginName, BotAction actions)
        {
            PluginName = pluginName;
            Actions = actions;
        }
    }
}
