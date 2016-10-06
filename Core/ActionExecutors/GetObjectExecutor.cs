using System;
using System.Globalization;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Получить информация об указанном объекте
    /// </summary>
    public class GetObjectExecutor : BaseExecutor
    {
        /// <summary>
        /// Исполняет метод получения информации об указанном объекте
        /// </summary>
        /// <param name="actions">Описание объекта, который необходимо получить</param>
        /// <param name="previousResult">Результат вызова предыдущего действия</param>
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

            if (actions.Count>1)
                throw new Exception("Получить можно только один объект.");

            throw new NotImplementedException();
        }

        public override IExecutorResult Invoke(IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }
    }
}
