using System;
using System.Threading;
using Core.ActionExecutors.PreviousResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;

namespace Core.ActionExecutors
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
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IBasePreviousResult Invoke(ListAction actions, IBasePreviousResult previousResult = null)
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
                        winInfo = windowsProc.GetWinInfo(action.WinTitle, action.SearchParam);
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