using System;
using Executor.ConfigEntity.ActionObjects;
using Executor.Core;
using Executor.Helpers;

namespace Executor.ActionExecutors
{
    /// <summary>
    /// Поддельный класс исполнитель :)
    /// </summary>
    internal sealed class MockExecutor : BaseExecutor
    {

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <returns></returns>
        public override Boolean Invoke(ListAction actions)
        {
            Print(new { Date = DateTime.Now.ToString(), Message = $"{nameof(MockExecutor)}.{nameof(Invoke)}({actions.ToJson(false)})", Status = EStatus.Info }, false);
            return true;
        }
    }
}
