using System;
using System.Globalization;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
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

        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        public new static ActionType ActionType => ActionType.GetObject;

        readonly IWindowsProc _windowsProc = AppContext.Get<IWindowsProc>();

        /// <summary>
        /// Исполняет метод получения информации об указанном объекте
        /// </summary>
        /// <param name="actions">Описание объекта, который необходимо получить</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат вызова предыдущего действия</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(IActionsContainer actions, ref bool isAbort, IExecutorResult previousResult = null)
        {
            Print(new
            {
                Date = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Message = new
                {
                    func = $"{GetType().Name}.{nameof(Invoke)}",
                    param =
                    $"{nameof(actions)}: {actions.ToJson(false, false)} ;{nameof(previousResult)}: {previousResult?.ToJson(false, false)})"
                },
                Status = EStatus.Info
            }, false);

            if (actions.SubActions.Count > 1)
                throw new Exception("Получить можно только один объект.");

            ObjectExecutorResult res = null;

            var objectAct = actions.SubActions.Cast<GetObjectAct>().FirstOrDefault();

            if ((objectAct == null || objectAct.PrevResult) && previousResult?.State == EResultState.Success)
                res = (ObjectExecutorResult)Invoke(ref isAbort, previousResult);

            if (res != null && res.State == EResultState.Success)
            {
                if (objectAct?.SetFocus == true)
                    try { _windowsProc.ShowWindow(res.ExecutorResult.Descriptor); } catch { }
                return res;
            }
            if (objectAct != null)
            {
                if (!objectAct.ObjectPos.IsEmpty)
                    res = new ObjectExecutorResult(_windowsProc.GetObjectFromPoint(objectAct.ObjectPos));

                if (res != null)
                {
                    if (objectAct.SetFocus)
                        try { _windowsProc.ShowWindow(res.ExecutorResult.Descriptor); } catch { }
                    return res;
                }
            }
            return new BaseExecutorResult(EResultState.NoResult);
        }

        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            ObjectExecutorResult res = null;
            if (previousResult != null)
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

                            res = new ObjectExecutorResult(_windowsProc.GetObjectFromPoint(expWin.ExecutorResult.Pos));
                        }
                        break;
                    case nameof(CurrentMousePosExecutorResult):
                        {
                            var mousePos = (CurrentMousePosExecutorResult)previousResult;
                            if (mousePos.State != EResultState.Success)
                                throw new Exception("Ошибка относительного позиционирования, CurrentMousePosExecutorResult не валиден");
                            var currentPos = mousePos.ExecutorResult;
                            res = new ObjectExecutorResult(_windowsProc.GetObjectFromPoint(currentPos));
                        }
                        break;
                    case nameof(ObjectExecutorResult):
                        {
                            var mousePos = (ObjectExecutorResult)previousResult;
                            if (mousePos.State != EResultState.Success)
                                throw new Exception("Ошибка относительного позиционирования, ObjectExecutorResult не валиден");
                            var currentPos = mousePos.ExecutorResult.Pos;
                            var info = _windowsProc.GetObjectFromPoint(currentPos);
                            if (info != null)
                                _windowsProc.ShowWindow(info.Descriptor);
                            res = new ObjectExecutorResult(info);
                        }
                        break;
                    case nameof(BaseExecutorResult):
                    {
                      return Invoke(ref isAbort, AppContext.Get<IActionExecutorFactory>()
                            .GetExecutorAction(ActionType.GetMousePos)
                            .Invoke(ref isAbort, previousResult));
                    }
                    default:
                        break;
                }
            }
            return res ?? previousResult ?? new BaseExecutorResult(EResultState.NoResult);
        }
    }
}
