using System;
using System.Collections.Generic;
using System.Linq;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using Core.Resources;

namespace Core.Core
{
    public interface IConfigValidator
    {
        /// <summary>
        /// Получить список ошибок конфигурации
        /// </summary>
        /// <returns></returns>
        List<String> GetErrorList(ListBotAction actions);
    }

    public class ConfigValidator : IConfigValidator
    {
        /// <summary>
        /// Получить список ошибок конфигурации
        /// </summary>
        /// <returns></returns>
        public List<String> GetErrorList(ListBotAction actions)
        {
            var list = new List<String>();
            return GetErrorListInternal(actions, ref list);
        }

        private List<String> GetErrorListInternal(ListBotAction actions, ref List<String> list)
        {
            foreach (var action in actions)
            {
                if (!action.IsMultiAct && action.SubActions.Count > 1)
                {
                    list.Add(String.Format(CoreText.ConfigValidator_Invalid_action_one_sub_action,
                        action.ActionType));
                    continue;
                }
                if (!action.IsValid)
                {
                    list.Add(String.Format(CoreText.ConfigValidator_Invalid_action, action.ActionType));
                    continue;
                }
                switch (action.ActionType)
                {
                    case ActionType.GOTO:
                        {
                            var goToAct = action.SubActions.FirstOrDefault() as GoToAct;
                            if (goToAct != null && !FindLabel(actions, goToAct.LabelName))
                                list.Add(String.Format(CoreText.ConfigValidator_Invalid_action_Label_not_found,
                                    action.ActionType, goToAct.LabelName));
                            if (String.IsNullOrEmpty(goToAct?.LabelName))
                                list.Add("GOTO: " + CoreText.ConfigValidator_GetErrorListInternal_Error_EmptyLabelName);
                        }
                        break;
                    case ActionType.Label:
                        {
                            var labelAct = action.SubActions.FirstOrDefault() as LabelAct;
                            var count =
                                actions
                                    .Count(x => x.ActionType == ActionType.Label &&
                                            ((LabelAct)x.SubActions.FirstOrDefault())?.LabelName == labelAct?.LabelName);
                            if (count > 1)
                                list.Add(
                                    String.Format(
                                        CoreText.ConfigValidator_Invalid_action_declared_more_than_once_in_the_same_scope,
                                        action.ActionType, labelAct?.LabelName));
                            if (String.IsNullOrEmpty(labelAct?.LabelName))
                                list.Add("Label: " + CoreText.ConfigValidator_GetErrorListInternal_Error_EmptyLabelName);
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
                                    list.Add(string.Format(CoreText.ConfigValidator_Invalid_action_Label_not_found,
                                        action.ActionType, ifAction.SuccessLabel));
                                if (!FindLabel(actions, ifAction.FailLabel))
                                    list.Add(string.Format(CoreText.ConfigValidator_Invalid_action_Label_not_found,
                                        action.ActionType, ifAction.FailLabel));
                            }
                        }
                        break;
                }
            }
            return list;
        }

        private Boolean FindLabel(ListBotAction actions, String label)
        {
            return actions.Where(t => t.ActionType == ActionType.Label)
                .Any(t => label.Equals(((LabelAct)t.SubActions.FirstOrDefault())?.LabelName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
