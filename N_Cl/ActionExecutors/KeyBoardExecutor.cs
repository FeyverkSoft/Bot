using System;
using Executor.ConfigEntity.ActionObjects;
using Executor.Core;
using Executor.Handlers;

namespace Executor.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия одиночного нажатия клавишы на клавиатуре
    /// </summary>
    internal sealed class KeyBoardExecutor : BaseExecutor
    {
        private IKeyBoard KeyBoard { get; set; } = new KeyBoard();
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
                foreach (KeyBoardAct action in actions)
                {
                    KeyBoard.PressKey(action.Key);
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