using System;
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
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IBasePreviousResult Invoke(ListAction actions, IBasePreviousResult previousResult = null)
        {
            Print(new { Date = DateTime.Now.ToString(), Message = $"{nameof(MockExecutor)}.{nameof(Invoke)}({actions.ToJson(false)}, {previousResult?.ToJson()??"--"})", Status = EStatus.Info }, false);
            return true;
        }
    }
}
