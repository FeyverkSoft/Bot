using System.Drawing;

namespace Core.ActionExecutors.ExecutorResult
{
    /// <summary>
    /// результат исполнения получения скриншота
    /// </summary>
    public class ScreenShotExecutorResult : BaseExecutorResult
    {

        public Bitmap Bitmap { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap">скринчик</param>
        /// <param name="executorState">Статус результата исполнения</param>
        public ScreenShotExecutorResult(Bitmap bitmap, EResultState executorState = EResultState.Success) : base(executorState)
        {
            Bitmap = bitmap;
        }
    }
}
