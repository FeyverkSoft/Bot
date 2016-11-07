using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Информация для вызова плагина
    /// </summary>
   [LocDescription("PluginInvokeAct", typeof(Resources.CoreText))]
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
        [LocDescription("PluginInvokeAct_PluginName", typeof(Resources.CoreText))]
        [DataMember]
        public String PluginName { get; set; }

        /// <summary>
        /// Список действий которые необходимо передать плагину
        /// </summary>
        [DataMember]
        [LocDescription("PluginInvokeAct_Actions", typeof(Resources.CoreText))]
        public BotAction Actions { get; set; } = new BotAction(ActionType.PluginAct);

        public PluginInvokeAct(String pluginName, BotAction actions)
        {
            PluginName = pluginName;
            Actions = actions;
        }
        public PluginInvokeAct() { }
    }
}
