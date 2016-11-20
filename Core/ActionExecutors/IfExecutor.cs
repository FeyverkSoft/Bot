using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Core;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Проверяет условие, и возвращает результат проверки
    /// </summary>
    public class IfExecutor : BaseExecutor
    {

        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        public new static ActionType ActionType => ActionType.If;

        /// <summary>
        /// Исполняет проверку условия, и возвращает результат проверки
        /// </summary>
        /// <param name="actions">Список проверок</param>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат проверки</param>
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
            if (actions.SubActions.Count == 0 && previousResult == null)
                return new BooleanExecutorResult(false);


            if (actions.SubActions.Count > 1)
                throw new Exception("Проверка может быть только одна");
            Boolean result = true;

            var action = actions.SubActions.Cast<IfAction>().First();

            /*if (previousResult == null || !previousResult.GetType().Name.ToLower().Contains(action.PrevResType.ToLower()))
                return new BooleanExecutorResult(false, EResultState.Success);
                */

            result = СonditionsParse(action.Сonditions?.Params, previousResult);

            return new BooleanExecutorResult(result);
        }

        /// <summary>
        /// Не поддерживается
        /// </summary>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат проверки</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ref bool isAbort, IExecutorResult previousResult = null)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Разбор условия на лексемы и проверка, пока что заглушка
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private Boolean СonditionsParse(List<ConditionalParam> conditions, IExecutorResult previousResult)
        {
            var result = true;
            var type = previousResult.GetType();
            foreach (var conditionalParam in conditions)
            {
                var val = getValue(conditionalParam.Name, previousResult, 0);
                val = val;
            }
            return result;
        }

        private Object getValue(String propName, object previousResult, Int32 deep)
        {
            var type = previousResult.GetType();
            var path = propName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (deep < path.Length)
            {
                if (deep == path.Length - 1)
                    return type.GetProperty(path[deep]).GetValue(previousResult);
                else
                    return getValue(propName, type.GetProperty(path[deep]).GetValue(previousResult), deep + 1);
            }
            return null;
        }
    }
}
