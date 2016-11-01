using System;
using System.Collections.Generic;
using System.Linq;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;

namespace Core.Core
{
    public static class ConfigValidator
    {
        /// <summary>
        /// Получить список ошибок конфигурации
        /// </summary>
        /// <returns></returns>
        public static List<String> GetErrorList(ListBotAction actions)
        {
            var list = new List<String>();
            return GetErrorListInternal(actions, ref list);
        }

        private static List<String> GetErrorListInternal(ListBotAction actions, ref List<String> list)
        {
            foreach (var action in actions)
            {
                if (!action.IsMultiAct && action.SubActions.Count > 1)
                {
                    list.Add($"Invalid action {action.ActionType}; For this action can only be one sub-action.");
                    continue;
                }
                if (!action.IsValid)
                {
                    list.Add($"Invalid action {action.ActionType}");
                    continue;
                }
                switch (action.ActionType)
                {
                    case ActionType.GOTO:
                        {
                            var goToAct = action.SubActions.FirstOrDefault() as GoToAct;
                            if (goToAct != null && !FindLabel(actions, goToAct.LabelName))
                                list.Add($"Invalid action {action.ActionType}, Label {goToAct.LabelName} not found");
                        }
                        break;
                    case ActionType.Loop:
                        {
                            foreach (var subAct in action.SubActions.Cast<LoopAct>())
                            {
                                GetErrorListInternal(subAct.Actions, ref list);
                            }
                        }
                        break;
                    case ActionType.If:
                        {
                            var ifAction = action.SubActions.FirstOrDefault() as IfAction;
                            if (ifAction != null)
                            {
                                if (!FindLabel(actions, ifAction.SuccessLabel))
                                    list.Add($"Invalid action {action.ActionType}, Label {ifAction.SuccessLabel} not found");
                                if (!FindLabel(actions, ifAction.FailLabel))
                                    list.Add($"Invalid action {action.ActionType}, Label {ifAction.FailLabel} not found");
                            }
                        }
                        break;
                }
            }
            return list;
        }

        private static Boolean FindLabel(ListBotAction actions, String label)
        {
            return actions.Where(t => t.ActionType == ActionType.Label)
                .Any(t => label.Equals(((LabelAct)t.SubActions.FirstOrDefault())?.LabelName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
