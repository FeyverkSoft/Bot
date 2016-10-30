using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;

namespace WpfExecutor.Model.Add
{
    /// <summary>
    /// Настройка и генерация элементов типа IAction
    /// </summary>
    public sealed class AddActionViewModel : BaseViewModel
    {
        private Tuple<String, Object> _currentType;

        /// <summary>
        /// Текущий выбранный тип действия
        /// </summary>
        public Tuple<String, Object> CurrentType
        {
            get { return _currentType; }
            set
            {
                _currentType = value;
                Refresh();
            }
        }

        public IAction CurrentAct { get; set; }

        public void Refresh()
        {
           var ait = (ActionType)CurrentType.Item2;
            switch (ait)
            {
                case ActionType.MouseMove:
                    CurrentAct = new MouseMoveAct();
                    break;
                case ActionType.MouseSetPos:
                    CurrentAct = new MouseSetPosAct();
                    break;
                case ActionType.MouseRClick:
                case ActionType.MouseRPress:
                case ActionType.MouseRUp:
                case ActionType.MouseLPress:
                case ActionType.MouseLUp:
                case ActionType.MouseLClick:
                case ActionType.GetMousePos:
                    CurrentAct = null;
                    break;
                case ActionType.KeyBoard:
                case ActionType.KeyBoardKeys:
                    CurrentAct = new KeyBoardAct();
                    break;
                case ActionType.Sleep:
                    CurrentAct = new SleepAct();
                    break;
                case ActionType.Loop:
                    CurrentAct = new LoopAct();
                    break;
                case ActionType.PluginInvoke:
                    CurrentAct = new PluginInvokeAct();
                    break;
                case ActionType.If:
                    CurrentAct = new IfAction();
                    break;
                case ActionType.ExpectWindow:
                    CurrentAct = new ExpectWindowAct();
                    break;
                case ActionType.GetObject:
                    CurrentAct = new GetObjectAct();
                    break;
                case ActionType.GetScreenshot:
                    CurrentAct = new ScreenShotAct();
                    break;
                case ActionType.Mock:
                    CurrentAct = new MockAction();
                    break;
                case ActionType.PluginAct:
                    CurrentAct = null;
                    break;
                case ActionType.SendMessage:
                    CurrentAct = new SendMessageAct();
                    break;
                case ActionType.Label:
                    CurrentAct = new LabelAct();
                    break;
                case ActionType.GOTO:
                    CurrentAct = new GoToAct();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
