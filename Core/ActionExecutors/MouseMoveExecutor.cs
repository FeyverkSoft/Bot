using System;
using System.Globalization;
using System.Threading;
using Core.ActionExecutors.PreviousResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия перемешения позиции указателя мышки
    /// </summary>
    internal sealed class MouseMoveExecutor : BaseExecutor
    {
        private IMouse Mouse { get; set; } = new Mouse();
        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IPreviousResult Invoke(ListAction actions, IPreviousResult previousResult = null)
        {
            Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), Message = $"{GetType().Name}.{nameof(Invoke)}(actions.Count:{actions?.Count ?? -1})", Status = EStatus.Info }, false);
            try
            {
                if (actions != null)
                    foreach (MouseMoveAct action in actions)
                    {
                        Mouse.MouseMove(action.Dx, action.Dy);
                        Thread.Sleep(150);//пауза перед началом нового движения
                    }
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), ex });
                return new BaseExecutorResult(EResultState.Error & EResultState.NoResult);
            }
            return previousResult?? new BaseExecutorResult() ;
        }
    }
}