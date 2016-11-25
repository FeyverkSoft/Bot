using System;
using System.Globalization;
using System.Windows;
using Core.ConfigEntity;
using WpfConverters.Converters;

namespace WpfExecutor.Converter
{
    public class ImageActConverter : BaseValueConverterExtension
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is ActionType)
            {
                var act = ((ActionType) value);
                var group = String.Empty;
                switch (act)
                {
                    case ActionType.MouseMove:
                    case ActionType.MouseSetPos:
                    case ActionType.MouseRClick:
                    case ActionType.MouseRPress:
                    case ActionType.MouseRUp:
                    case ActionType.MouseLPress:
                    case ActionType.MouseLUp:
                    case ActionType.MouseLClick:
                    case ActionType.GetMousePos:
                        group = "Mouse";
                        break;
                    case ActionType.KeyBoardPressKey:
                    case ActionType.KeyBoardShortcut:
                    case ActionType.KeyBoardAction:
                        group = "Keyboard";
                        break;
                    case ActionType.Sleep:
                        group = "Sleep";
                        break;
                    case ActionType.Loop:
                        group = "Loop";
                        break;
                    case ActionType.PluginInvoke:
                        group = "Plugin";
                        break;
                    case ActionType.If:
                        group = "If";
                        break;
                    case ActionType.ExpectWindow:
                        group = "Win";
                        break;
                    case ActionType.GetObject:
                        group = "Object";
                        break;
                    case ActionType.GetScreenshot:
                        group = "Screenshot";
                        break;
                    case ActionType.Mock:
                        break;
                    case ActionType.PluginAct:
                        group = "Plugin";
                        break;
                    case ActionType.SendMessage:
                        group = "Message";
                        break;
                    case ActionType.Label:
                        group = "Label";
                        break;
                    case ActionType.GOTO:
                        group = "Goto";
                        break;
                    case ActionType.Stack:
                        group = "Stack";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (!String.IsNullOrEmpty(group))
                {
                    return Application.Current.TryFindResource($"{group}Icon");
                }
            }
            return value;
        }
    }
}
