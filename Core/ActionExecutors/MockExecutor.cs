using System;
using System.Globalization;
using Core.ActionExecutors.PreviousResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Поддельный класс исполнитель :)
    /// </summary>
    internal sealed class MockExecutor : BaseExecutor
    {

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IPreviousResult Invoke(ListAction actions, IPreviousResult previousResult = null)
        {
            Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), Message = $"{nameof(MockExecutor)}.{nameof(Invoke)}({actions.ToJson(false)}, {previousResult?.ToJson() ?? "--"})", Status = EStatus.Info }, false);
            return previousResult ?? new BaseExecutorResult();
        }
    }
}
