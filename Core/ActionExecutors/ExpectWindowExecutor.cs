using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;
using Core.Helpers;

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
        IWindowsProc WindowsProc { get; set; } = AppContext.Get<IWindowsProc>();

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(IActionsContainer actions, ref bool isAbort, IExecutorResult previousResult = null)
        {
            Print(new
            {
                Date = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Message = new
                {
                    func = $"{GetType().Name}.{nameof(Invoke)}",
                    param = $"{nameof(actions)}: {actions.ToJson(false, false)})"
                },
                Status = EStatus.Info
            }, false);

            try
            {
                //:D как-то тупо, несколько действий сна подряд....
                if (actions != null)
                {
                    WinInfo winInfo = null;
                    foreach (var action in actions.SubActions.Cast<ExpectWindowAct>())
                    {
                        do
                        {
                            if (isAbort)
                                return new BaseExecutorResult(EResultState.NoResult);
                            Thread.Sleep(500);
                            winInfo = WindowsProc.GetWinInfo(action.WinTitle, action.SearchParam);
                        } while (!winInfo.IsFound);

                        if (action.SetFocus)
                        {
                            WindowsProc.ShowWindow(winInfo.Descriptor);
                        }
                    }
                    return new ExpectWindowExecutorResult(winInfo);
                }
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), ex });
                return new BaseExecutorResult(EResultState.Error & EResultState.NoResult);
            }
            return previousResult ?? new BaseExecutorResult();
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }
    }
}