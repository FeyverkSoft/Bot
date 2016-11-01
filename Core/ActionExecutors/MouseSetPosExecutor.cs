using System;
using System.Globalization;
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
    /// Исполнитель действия установки указателя мыши в нужную позицию
    /// </summary>
    internal sealed class MouseSetPosExecutor : BaseExecutor
    {
        private IMouse Mouse { get; set; } = AppContext.Get<IMouse>();
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
                    param = $"{nameof(actions)}: {actions.ToJson(false, false)} ;{nameof(previousResult)}: {previousResult?.ToJson(false, false)})"
                },
                Status = EStatus.Info
            }, false);
            try
            {
                if (actions != null)
                    foreach (var action1 in actions.SubActions)
                    {
                        var action = (MouseSetPosAct) action1;
                        //приоритет
                        //сначало относительно названия окна
                        //потом относительно окна полученного на предыдущем шаге
                        //потом обсолютная позиция
                        if (action.Relatively && !String.IsNullOrEmpty(action.RelativelyWindowName))
                        {
                            var winInfo = WindowsProc.GetWinInfo(action.RelativelyWindowName);
                            Mouse.MouseSetPos(action.X + (winInfo.Pos.X > 0 ? winInfo.Pos.X : 0), action.Y + (winInfo.Pos.Y > 0 ? winInfo.Pos.Y : 0));
                        }
                        else if (action.Relatively && previousResult != null)
                        {
                            //выбор особых сценариев дляразных результатов
                            //например перемещение относительно окна или еще чего то
                            switch (previousResult.GetType().Name)
                            {
                                case nameof(ExpectWindowExecutorResult):
                                    {
                                        var expWin = (ExpectWindowExecutorResult)previousResult;
                                        if (expWin.State != EResultState.Success && expWin.ExecutorResult == null)
                                            throw new Exception(
                                                "Ошибка относительного позиционирования, ExpectWindowExecutorResult не валиден");
                                        Mouse.MouseSetPos(
                                            action.X + (expWin.ExecutorResult.Pos.X > 0 ? expWin.ExecutorResult.Pos.X : 0),
                                            action.Y + (expWin.ExecutorResult.Pos.Y > 0 ? expWin.ExecutorResult.Pos.Y : 0));
                                        break;
                                    }
                                case nameof(CurrentMousePosExecutorResult):
                                    {
                                        var mousePos = (CurrentMousePosExecutorResult)previousResult;
                                        if (mousePos.State != EResultState.Success)
                                            throw new Exception(
                                                "Ошибка относительного позиционирования, CurrentMousePosExecutorResult не валиден");
                                        var currentPos = mousePos.ExecutorResult;
                                        Mouse.MouseSetPos(currentPos.X + action.X, currentPos.Y + action.Y);
                                    }
                                    break;
                                case nameof(ObjectExecutorResult):
                                    {
                                        var mousePos = (ObjectExecutorResult)previousResult;
                                        if (mousePos.State != EResultState.Success)
                                            throw new Exception("Ошибка относительного позиционирования, ObjectExecutorResult не валиден");
                                        var currentPos = mousePos.ExecutorResult.Pos;
                                        Mouse.MouseMove(currentPos.X + action.X, currentPos.Y + action.Y);//Дич, аналогично тому что просто вызвать, но но, при передаче тунельемпараметра может быть эффект :)
                                    } break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Mouse.MouseSetPos(action.X, action.Y);
                        }
                        Thread.Sleep(150);
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