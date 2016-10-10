using System;
using System.Linq;
using Plugin;

namespace Core.ActionExecutors.Factory
{
    /// <summary>
    /// Фабрика для плагинов по умолчанию
    /// </summary>
    internal class DefaultPluginFactory : IPluginFactory
    {
        /// <summary>
        /// Возвращает плагин по его названию
        /// </summary>
        /// <param name="pluginName">Название плагина для которого происходит поиск фабрики</param>
        /// <returns></returns>
        public IPlugin GetPlugin(String pluginName)
        {
            if (String.IsNullOrEmpty(pluginName))
                throw new ArgumentNullException(nameof(pluginName));
            return Assemblys.PluginsList.First(x => x.Name.Equals(pluginName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}