using System;
using System.Globalization;
using System.Linq;
using Core.ActionExecutors.PreviousResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия одновременно нажатия нескольких клавиш на клавиатуре
    /// Например ctr+a
    /// </summary>
    internal sealed class KeyBoardKeysExecutor : BaseExecutor
    {
        private IKeyBoard KeyBoard { get; set; } = new KeyBoard();
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
                    KeyBoard.PressKeys(actions.Select(x => ((KeyBoardAct)x).Key).ToList());
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), ex });
                return new BaseExecutorResult(EResultState.Error & EResultState.NoResult);
            }
            return previousResult?? new BaseExecutorResult();
        }
    }
}