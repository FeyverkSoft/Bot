using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Helpers;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Информация для вызова плагина
    /// </summary>
   [LocDescription("PluginInvokeAct")]
    public class PluginInvokeAct : IAction
    {
        /// <summary>
        /// Наименование плагина
        /// </summary>
        [LocDescription("PluginInvokeAct_PluginName")]
        [DataMember]
        public String PluginName { get; private set; }

        /// <summary>
        /// Список действий которые необходимо передать плагину
        /// </summary>
        [DataMember]
        [LocDescription("PluginInvokeAct_Actions")]
        public BotAction Actions { get; set; }

        public PluginInvokeAct(String pluginName, BotAction actions)
        {
            PluginName = pluginName;
            Actions = actions;
        }

        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return this.GetString();
        }
    }
}
