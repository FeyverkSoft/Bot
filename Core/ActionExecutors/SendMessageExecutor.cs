using System;
using System.Globalization;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Handlers;
using Core.Helpers;
using Core.Message;
using LogWrapper;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Исполнитель отправки сообщений
    /// </summary>
    internal sealed class SendMessageExecutor : BaseExecutor
    {
        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, ref bool isAbort, IExecutorResult previousResult = null)
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
                    foreach (var action in actions.Cast<SendMessageAct>())
                    {
                        var sender = MessageSenderFactory.GetSender(action.MessageType);
                        var res = sender.SendMessage(
                               $"{action.Body}</br>{(action.IncludePrevRes && previousResult != null ? previousResult.ToJson().Replace("\n", "</br>") : "")}",
                               action.Recipient);
                        if (res.Status != EMessageStatus.Success)
                            Log.WriteLine(res, LogLevel.Error);
                    }
            }
            catch (Exception ex)
            {
                Print(new { Date = DateTime.Now.ToString(CultureInfo.InvariantCulture), ex });
                return new BaseExecutorResult(EResultState.Error & EResultState.NoResult);
            }
            return previousResult ?? new BaseExecutorResult();
        }

        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }
    }
}