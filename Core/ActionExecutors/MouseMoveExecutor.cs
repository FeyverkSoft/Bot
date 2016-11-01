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
    /// Исполнитель действия перемешения позиции указателя мышки
    /// </summary>
    internal sealed class MouseMoveExecutor : BaseExecutor
    {
        private IMouse Mouse { get; set; } = AppContext.Get<IMouse>();

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
                        var action = (MouseMoveAct) action1;
                        if (action.ToObject && previousResult != null)
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
                                        var win = expWin.ExecutorResult;

                                        var currentPos = Mouse.GetCurrentPos();
                                        Int32 x = (win.Pos.X - currentPos.X), y = (win.Pos.Y - currentPos.Y);
                                        Mouse.MouseMove(x + action.Dx, y + action.Dy);
                                    }
                                    break;
                                case nameof(CurrentMousePosExecutorResult):
                                    {
                                        var mousePos = (CurrentMousePosExecutorResult)previousResult;
                                        if (mousePos.State != EResultState.Success)
                                            throw new Exception(
                                                "Ошибка относительного позиционирования, CurrentMousePosExecutorResult не валиден");
                                        var pos = mousePos.ExecutorResult;
                                        var currentPos = Mouse.GetCurrentPos();
                                        Int32 x = (currentPos.X - pos.X), y = (currentPos.Y - pos.Y);
                                        Mouse.MouseMove(x + action.Dx, y + action.Dy);
                                    }
                                    break;
                                case nameof(ObjectExecutorResult):
                                    {
                                        var mousePos = (ObjectExecutorResult)previousResult;
                                        if (mousePos.State != EResultState.Success)
                                            throw new Exception("Ошибка относительного позиционирования, ObjectExecutorResult не валиден");
                                        var pos = mousePos.ExecutorResult.Pos;

                                        var currentPos = Mouse.GetCurrentPos();
                                        Int32 x = (currentPos.X - pos.X), y = (currentPos.Y - pos.Y);
                                        Mouse.MouseMove(x + action.Dx, y + action.Dy);

                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (!action.ToObject)
                        {
                            Mouse.MouseMove(action.Dx, action.Dy);
                        }
                        else throw new Exception("Не указан объект к которому необходимо переместить указатель, или не указаны координаты");
                        Thread.Sleep(150);//пауза перед началом нового движения
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