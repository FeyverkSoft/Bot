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
                        if (action.ToObject && previousResult != null)
                        {
                            //выбор особых сценариев дляразных результатов
                            //например перемещение относительно окна или еще чего то
                            switch (previousResult.GetType().Name)
                            {
                                case nameof(ExpectWindowExecutorResult):
                                    var expWin = (ExpectWindowExecutorResult)previousResult;
                                    if (expWin.State != EResultState.Success && expWin.ExecutorResult == null)
                                        throw new Exception("Ошибка относительного позиционирования, ExpectWindowExecutorResult не валиден");
                                    var win = expWin.ExecutorResult;

                                    var currentPos = Mouse.GetCurrentPos();
                                    Int32 x = (currentPos.X - win.PosX), y = (currentPos.Y - win.PosY);
                                    Mouse.MouseSetPos(x + action.Dx, y + action.Dy);

                                    break;
                                default:
                                    throw new NotImplementedException();
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
    }
}