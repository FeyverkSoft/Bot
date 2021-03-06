﻿using System;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;

namespace Core.Plugin
{
    public interface IPlugin
    {
        String Name { get; }

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        IExecutorResult Invoke(BotAction actions, IExecutorResult previousResult = null);

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        IExecutorResult Invoke(IExecutorResult previousResult = null);
        /// <summary>
        /// Отображать меню или нет?
        /// </summary>
        Boolean ShowMenue { get; }
        /// <summary>
        /// Меню которое предоставляет бот
        /// </summary>
        PluginMenuItemModel Menu { get; }
    }

}
