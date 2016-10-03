﻿using System;
using System.Globalization;
using System.Threading;
using Core.ActionExecutors.PreviousResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия установки указателя мыши в нужную позицию
    /// </summary>
    internal sealed class MouseSetPosExecutor : BaseExecutor
    {
        private IMouse Mouse { get; set; } = new Mouse();
        IWindowsProc WindowsProc { get; set; } = new WindowsProc();
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
                    foreach (MouseSetPosAct action in actions)
                    {
                        //приоритет
                        //сначало относительно названия окна
                        //потом относительно окна полученного на предыдущем шаге
                        //потом обсолютная позиция
                        if (action.Relatively && !String.IsNullOrEmpty(action.RelativelyWindowName))
                        {
                            var winInfo = WindowsProc.GetWinInfo(action.RelativelyWindowName);
                            Mouse.MouseSetPos(action.X + (winInfo.PosX > 0 ? winInfo.PosX : 0), action.Y + (winInfo.PosY > 0 ? winInfo.PosY : 0));
                        }
                        else if (action.Relatively && previousResult != null)
                        {
                            //выбор особых сценариев дляразных результатов
                            //например перемещение относительно окна или еще чего то
                            switch (previousResult.GetType().Name)
                            {
                                case nameof(ExpectWindowExecutorResult):
                                    var expWin = (ExpectWindowExecutorResult)previousResult;
                                    if (expWin.State != EResultState.Success && expWin.ExecutorResult == null)
                                        throw new Exception("Ошибка относительного позиционирования, ExpectWindowExecutorResult не валиден");
                                    Mouse.MouseSetPos(action.X + (expWin.ExecutorResult.PosX > 0 ? expWin.ExecutorResult.PosX : 0), action.Y + (expWin.ExecutorResult.PosY > 0 ? expWin.ExecutorResult.PosY : 0));
                                    break;
                                default:
                                    throw new NotImplementedException();
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
    }
}