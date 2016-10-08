using System;
using System.Globalization;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Получить информация об указанном объекте
    /// </summary>
    public class GetObjectExecutor : BaseExecutor
    {
        readonly IWindowsProc _windowsProc = new NativeWindowsProc();
        /// <summary>
        /// Исполняет метод получения информации об указанном объекте
        /// </summary>
        /// <param name="actions">Описание объекта, который необходимо получить</param>
        /// <param name="previousResult">Результат вызова предыдущего действия</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null)
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

            if (actions.Count > 1)
                throw new Exception("Получить можно только один объект.");

            if (actions.Count == 0 && previousResult != null)
            {
                //выбор особых сценариев дляразных результатов
                //например перемещение относительно окна или еще чего то
                switch (previousResult.GetType().Name)
                {
                    case nameof(ExpectWindowExecutorResult):
                        {
                            var expWin = (ExpectWindowExecutorResult)previousResult;

                            if (expWin.State != EResultState.Success && expWin.ExecutorResult == null)
                                throw new Exception("Ошибка относительного позиционирования, ExpectWindowExecutorResult не валиден");

                            return new ObjectExecutorResult(_windowsProc.GetObjectFromPoint(expWin.ExecutorResult.Pos));
                        }
                    case nameof(CurrentMousePosExecutorResult):
                        {
                            var mousePos = (CurrentMousePosExecutorResult)previousResult;
                            if (mousePos.State != EResultState.Success)
                                throw new Exception("Ошибка относительного позиционирования, CurrentMousePosExecutorResult не валиден");
                            var currentPos = mousePos.ExecutorResult;
                            return new ObjectExecutorResult(_windowsProc.GetObjectFromPoint(currentPos));
                        }
                    case nameof(ObjectExecutorResult):
                        {
                            var mousePos = (ObjectExecutorResult)previousResult;
                            if (mousePos.State != EResultState.Success)
                                throw new Exception("Ошибка относительного позиционирования, ObjectExecutorResult не валиден");
                            var currentPos = mousePos.ExecutorResult.Pos;
                            return new ObjectExecutorResult(_windowsProc.GetObjectFromPoint(currentPos));
                        }
                    default:
                        throw new NotSupportedException(previousResult.GetType().Name);
                }
            }

            var objectAct = actions.Cast<GetObjectAct>().First();
            if (!objectAct.ObjectPos.IsEmpty)
                return new ObjectExecutorResult(_windowsProc.GetObjectFromPoint(objectAct.ObjectPos));

            return new BaseExecutorResult(EResultState.NoResult);
        }

        public override IExecutorResult Invoke(IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }
    }
}
