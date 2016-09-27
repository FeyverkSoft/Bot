using System;
using System.Threading;
using Executor.ConfigEntity.ActionObjects;
using Executor.Core;
using Executor.Handlers;

namespace Executor.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия ожидани появления окна с указанным заголовком, и передачи ему фокуса
    /// </summary>
    internal sealed class ExpectWindowExecutor : BaseExecutor
    {
        /// <summary>
        /// Функции для работы с окнами
        /// </summary>
        IWindowsProc windowsProc { get; set; } = new WindowsProc();

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
                //:D как-то тупо, несколько действий сна подряд....
                foreach (ExpectWindowAct action in actions)
                {
                    WinInfo winInfo = new WinInfo("", 0, 0, (IntPtr)0, false);
                    do
                    {
                        Thread.Sleep(500);
                        winInfo = windowsProc.GetWinInfo(action.WinTitle);
                    } while (!winInfo.IsFound);

                    if (action.SetFocus)
                    {
                        windowsProc.ShowWindow(winInfo.Descriptor);
                    }

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