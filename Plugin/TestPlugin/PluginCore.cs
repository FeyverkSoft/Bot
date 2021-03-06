﻿using System;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.Plugin;

namespace TestPlugin
{
    public class PluginCore : IPlugin
    {
        public String Name => "TestPlugin";

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public IExecutorResult Invoke(BotAction actions, IExecutorResult previousResult = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public IExecutorResult Invoke(IExecutorResult previousResult = null)
        {
            throw new NotImplementedException();
        }
    }
}
