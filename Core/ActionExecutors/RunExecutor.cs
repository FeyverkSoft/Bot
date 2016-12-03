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
using System.Diagnostics;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель действия запуска внешней программы
    /// </summary>
    internal sealed class RunExecutor : BaseExecutor
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        public new static ActionType ActionType => ActionType.Run;

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(IActionsContainer actions, ref Boolean isAbort, IExecutorResult previousResult = null)
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

                var runParam = actions.SubActions.Cast<RunAct>().FirstOrDefault();
                if (runParam == null)
                    throw new NullReferenceException(nameof(runParam));
                ProcessStartInfo psi;

                if ("smd".Equals(runParam.ProcessName, StringComparison.InvariantCultureIgnoreCase))
                    psi = new ProcessStartInfo(runParam.ProcessName, $"/k {runParam.ProcessKey}");
                else
                    psi = new ProcessStartInfo(runParam.ProcessName, runParam.ProcessKey);
                Process.Start(psi);

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
        public override IExecutorResult Invoke(ref Boolean isAbort, IExecutorResult previousResult = null)
        {
            Print(new
            {
                Date = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Message = new
                {
                    func = $"{GetType().Name}.{nameof(Invoke)}",
                    param = $"{nameof(previousResult)}: {previousResult?.ToJson(false, false)})"
                },
                Status = EStatus.Info
            }, false);
            throw new NotSupportedException();
        }
    }
}