using System;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ActionExecutors.Factory;
using Core.ConfigEntity.ActionObjects;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель плагина
    /// </summary>
    public class PluginInvokeExecutor : BaseExecutor
    {
        readonly IPluginFactory _pluginFactory = new DefaultPluginFactory();

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, ref bool isAbort, IExecutorResult previousResult = null)
        {
            if (!AppConfig.Instance.LoadPlugin)
                return previousResult;
            if (actions == null)
                throw new ArgumentNullException(nameof(actions));
            if (actions.Count > 1)
                throw new Exception("Для данного действия колличество параметров должно не привышать 1");
            var action = actions.Cast<PluginInvokeAct>().First();
            var plugin = _pluginFactory.GetPlugin(action.PluginName);
            return action.Actions == null ? plugin.Invoke(previousResult) : plugin.Invoke(action.Actions, previousResult);
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            throw new NotImplementedException();
        }
    }
}
