using System.Drawing;
using CommonLib.Attributes;

namespace Core.ActionExecutors.ExecutorResult
{
    /// <summary>
    /// результат исполнения получения скриншота
    /// </summary>
    public class BitmapExecutorResult : BaseExecutorResult
    {

        [VisualCtorIgnoreChildProp]
        public Bitmap Bitmap { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap">скринчик</param>
        /// <param name="executorState">Статус результата исполнения</param>
        public BitmapExecutorResult(Bitmap bitmap, EResultState executorState = EResultState.Success) : base(executorState)
        {
            Bitmap = bitmap;
        }
    }
}
