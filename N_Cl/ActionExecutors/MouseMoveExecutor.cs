using System;
using System.Threading;
using Executor.ConfigEntity.ActionObjects;
using Executor.Core;
using Executor.Handlers;

namespace Executor.ActionExecutors
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
        /// <param name="action">Список действи которые должен выполнить исполнитель</param>
        /// <returns></returns>
        public override Boolean Invoke(ListAction actions)
        {
            Print(new { Date = DateTime.Now.ToString(), Message = $"{GetType().Name}.{nameof(Invoke)}(actions.Count:{actions?.Count ?? -1})", Status = EStatus.Info }, false);
            try
            {
                foreach (MouseMoveAct action in actions)
                {
                    Mouse.MouseMove(action.Dx, action.Dy);
                    Thread.Sleep(150);//пауза перед началом нового движения
                }
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(), ex });
                return false;
            }
            return true;
        }
    }
}