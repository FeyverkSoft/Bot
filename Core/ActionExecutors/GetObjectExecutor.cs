using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Получить информация об указанном объекте
    /// </summary>
   public class GetObjectExecutor: BaseExecutor
    {
        /// <summary>
        /// Исполняет метод получения информации об указанном объекте
        /// </summary>
        /// <param name="actions">Описание объекта, который необходимо получить</param>
        /// <param name="previousResult">Результат вызова предыдущего действия</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null)
        {
            Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), Message = $"{GetType().Name}.{nameof(Invoke)}(actions.Count:{actions?.Count ?? -1})", Status = EStatus.Info }, false);

            throw new NotImplementedException();
        }
    }
}
