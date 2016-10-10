using System;
using Plugin;

namespace Core.ActionExecutors.Factory
{    
    /// <summary>
     /// Фабрика для плагинов по умолчанию
     /// </summary>
    internal interface IPluginFactory
    {
        /// <summary>
        /// Возвращает плагин по его названию
        /// </summary>
        /// <param name="pluginName">Название плагина для которого происходит поиск фабрики</param>
        /// <returns></returns>
        IPlugin GetPlugin(String pluginName);
    }
}