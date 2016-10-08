using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Функция получения скриншота
    /// </summary>
    public class GetScreenShotExecutor : BaseExecutor
    {
        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Вызвать выполнение действия у указанной фабрики
        /// </summary>
        /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(IExecutorResult previousResult = null)
        {
            if (previousResult == null)
                return new ScreenShotExecutorResult(ScreenCaptureHelper.GetScreenShot(0, 0, Screen.PrimaryScreen.Bounds.Width,
                    Screen.PrimaryScreen.Bounds.Height));
            return new BaseExecutorResult(EResultState.NoResult);
        }
    }
}
