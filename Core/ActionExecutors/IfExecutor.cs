﻿using System;
using System.Globalization;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Проверяет условие, и возвращает результат проверки
    /// </summary>
    public class IfExecutor : BaseExecutor
    {
        /// <summary>
        /// Исполняет проверку условия, и возвращает результат проверки
        /// </summary>
        /// <param name="actions">Список проверок</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат проверки</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(IActionsContainer actions, ref bool isAbort, IExecutorResult previousResult = null)
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
            if (actions.SubActions.Count == 0 && previousResult == null)
                return new BooleanExecutorResult(false);

            if (actions.SubActions.Count == 0 && previousResult is BooleanExecutorResult)
                return previousResult;

            if (actions.SubActions.Count == 0)
                return new BooleanExecutorResult(false);

            if (actions.SubActions.Count > 1)
                throw new Exception("Проверка может быть только одна");
            Boolean result = true;

            var action = actions.SubActions.Cast<IfAction>().First();

            if (previousResult == null || !previousResult.GetType().Name.ToLower().Contains(action.PrevResType.ToLower()))
                return new BooleanExecutorResult(false, EResultState.Success);


            var conditions = СonditionsParse(action.Сonditions);

            return new BooleanExecutorResult(result);
        }

        /// <summary>
        /// Не поддерживается
        /// </summary>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат проверки</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Разбор условия на лексемы, пока что заглушка
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private String СonditionsParse(String conditions)
        {
            return conditions;
        }
    }
}
