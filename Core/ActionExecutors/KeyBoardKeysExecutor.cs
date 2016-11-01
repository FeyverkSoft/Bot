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
    /// Исполнитель действия одновременно нажатия нескольких клавиш на клавиатуре
    /// Например ctr+a
    /// </summary>
    internal sealed class KeyBoardKeysExecutor : BaseExecutor
    {
        private IKeyBoard KeyBoard { get; set; } = AppContext.Get<IKeyBoard>();

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
                    KeyBoard.PressKeys(actions.SubActions.Select(x => ((KeyBoardAct)x).Key).ToList());
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), ex });
                return new BaseExecutorResult(EResultState.Error & EResultState.NoResult);
            }
            return previousResult?? new BaseExecutorResult();
        }

        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }
    }
}