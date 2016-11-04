using System;
using System.Runtime.Serialization;
using Core.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Информация для вызова плагина
    /// </summary>
   [LocDescription("PluginInvokeAct")]
    public class PluginInvokeAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.PluginInvoke;

        /// <summary>
        /// Наименование плагина
        /// </summary>
        [LocDescription("PluginInvokeAct_PluginName")]
        [DataMember]
        public String PluginName { get; set; }

        /// <summary>
        /// Список действий которые необходимо передать плагину
        /// </summary>
        [DataMember]
        [LocDescription("PluginInvokeAct_Actions")]
        public BotAction Actions { get; set; } = new BotAction(ActionType.PluginAct);

        public PluginInvokeAct(String pluginName, BotAction actions)
        {
            PluginName = pluginName;
            Actions = actions;
        }
        public PluginInvokeAct() { }
    }
}
