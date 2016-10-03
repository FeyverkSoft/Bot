using System;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;

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
