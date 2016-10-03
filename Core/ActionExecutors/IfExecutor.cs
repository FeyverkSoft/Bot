﻿using System;
using System.Globalization;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Проверяет условие, и возвращает результат проверки
    /// </summary>
    public class IfExecutor:BaseExecutor
    {
        /// <summary>
        /// Исполняет проверку условия, и возвращает результат проверки
        /// </summary>
        /// <param name="actions">Список проверок</param>
        /// <param name="previousResult">Результат проверки</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null)
        {
            Print(new
            {
                Date = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Message = new
                {
                    func = $"{GetType().Name}.{nameof(Invoke)}",
                    param = $"{nameof(actions)}: {actions.ToJson(false, false)} ;{nameof(previousResult)}: {previousResult?.ToJson(false, false)})"
                },
                Status = EStatus.Info
            }, false);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Не поддерживается
        /// </summary>
        /// <param name="previousResult">Результат проверки</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }
    }
}
