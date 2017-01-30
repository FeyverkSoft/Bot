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
            if (actions.SubActions.Count == 0 && previousResult == null)
                return new BooleanExecutorResult(false);


            if (actions.SubActions.Count > 1)
                throw new Exception("Проверка может быть только одна");
            Boolean result = true;

            var action = actions.SubActions.Cast<IfAction>().First();

            result = СonditionsParse(action.Сonditions?.Params, previousResult);

            return new BooleanExecutorResult(result);
        }

        /// <summary>
        /// Не поддерживается
        /// </summary>
        /// <param name="isAbort"></param>
        /// <param name="previousResult">Результат проверки</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ref Boolean isAbort, IExecutorResult previousResult = null)
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
            foreach (var conditionalParam in conditions)
            {
                var val = GetValue(conditionalParam.Name, previousResult, 0);
                result &= TestValue(val, conditionalParam.ConditionalValue, conditionalParam.Conditional);
            }
            return result;
        }

        private Object GetValue(String propName, Object previousResult, Int32 deep)
        {
            var type = previousResult.GetType();
            var path = propName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (deep < path.Length)
            {
                if (deep == path.Length - 1)
                    return type.GetProperty(path[deep]).GetValue(previousResult);
                else
                    return GetValue(propName, type.GetProperty(path[deep]).GetValue(previousResult), deep + 1);
            }
            return null;
        }

        /// <summary>
        /// Дичь, надо будет переписать, после того как пойму, как это должно вообще работать......
        /// В том числе вообще как этоот оператор должен работать
        /// </summary>
        /// <param name="value"></param>
        /// <param name="condValue"></param>
        /// <param name="cond"></param>
        /// <returns></returns>
        private Boolean TestValue(Object value, Object condValue, EConditional cond)
        {
            switch (cond)
            {
                case EConditional.Equal:
                    if (condValue?.ToString().ToLower() == "null")
                        return value == null;
                    return value == condValue;
                case EConditional.NotEqual:
                    if (condValue?.ToString().ToLower() == "null")
                        return value != null;
                    return value != condValue;
                case EConditional.IsGreaterThan:
                    if (value is String)
                        return (value as String).CompareTo(condValue) == 1;
                    return Decimal.Parse(value.ToString()).CompareTo(Decimal.Parse(condValue.ToString())) == 1;
                case EConditional.IsLessThan:
                    if (value is String)
                        return (value as String).CompareTo(condValue) < 0;
                    return Decimal.Parse(value.ToString()).CompareTo(Decimal.Parse(condValue.ToString())) < 0;
                case EConditional.IsLessThanOrEqual:
                    if (value is String)
                        return (value as String).CompareTo(condValue) <= 0;
                    return Decimal.Parse(value.ToString()).CompareTo(Decimal.Parse(condValue.ToString())) <= 0;
                case EConditional.IsGreaterThanOrEqual:
                    if (value is String)
                        return (value as String).CompareTo(condValue) >= 0;
                    return Decimal.Parse(value.ToString()).CompareTo(Decimal.Parse(condValue.ToString())) >= 0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cond), cond, null);
            }
        }
    }
}
