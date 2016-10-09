using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity.ActionObjects;
using Core.Handlers;
using Core.Helpers;

namespace Core.ActionExecutors
{
    /// <summary>
    /// Функция получения скриншота
    /// </summary>
    public class GetScreenShotExecutor : BaseExecutor
    {
        readonly IWindowsProc _win = new NativeWindowsProc();
        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public override IExecutorResult Invoke(ListAct actions, IExecutorResult previousResult = null)
        {
            if (actions == null)
                return Invoke(previousResult);
            if (actions.Count > 1)
                throw new Exception("Возможно выполнение только 1го действия");
            var act = actions.Cast<ScreenShotAct>().First();
            ScreenShotExecutorResult res;
            if (!act.Size.IsEmpty)
                res = new ScreenShotExecutorResult(ScreenCaptureHelper.GetScreenShot(act.Point.X, act.Point.Y, act.Size.WidthX, act.Size.HeightY));
            else
            {
                var width = (Screen.PrimaryScreen.Bounds.Width - act.Point.X);
                var height = (Screen.PrimaryScreen.Bounds.Height - act.Point.Y);
                res = new ScreenShotExecutorResult(ScreenCaptureHelper.GetScreenShot(act.Point.X, act.Point.Y,
                   width > 0 ? width : 1, height > 0 ? height : 1));
            }
            if (act.SaveFileParam != null)
            {
                var param = act.SaveFileParam;
                DirectoryHelper.CreateDirectory(param.Path);
                ImageFormat imgF;
                switch (param.Type.ToLower())
                {
                    case "tiff":
                        imgF = ImageFormat.Tiff;
                        break;
                    case "bmp":
                        imgF = ImageFormat.Bmp;
                        break;
                    case "jpeg":
                        imgF = ImageFormat.Jpeg;
                        break;
                    case "png":
                        imgF = ImageFormat.Png;
                        break;
                    default:
                        imgF = ImageFormat.Png;
                        break;
                }
                res.Bitmap.Save($"{param.Path}{param.Type}", imgF);
            }
            return res;
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
            ScreenShotExecutorResult res = null;

            //выбор особых сценариев дляразных результатов
            //например перемещение относительно окна или еще чего то
            switch (previousResult.GetType().Name)
            {
                case nameof(ExpectWindowExecutorResult):
                    {
                        var expWin = (ExpectWindowExecutorResult)previousResult;

                        if (expWin.State != EResultState.Success && expWin.ExecutorResult == null)
                            throw new Exception("Ошибка относительного позиционирования, ExpectWindowExecutorResult не валиден");
                        var currentPos = expWin.ExecutorResult;
                        res = new ScreenShotExecutorResult(ScreenCaptureHelper.GetScreenShot(currentPos.Pos.X, currentPos.Pos.Y, currentPos.Size.WidthX, currentPos.Size.HeightY));
                    }
                    break;
                case nameof(CurrentMousePosExecutorResult):
                    {
                        var mousePos = (CurrentMousePosExecutorResult)previousResult;
                        if (mousePos.State != EResultState.Success)
                            throw new Exception("Ошибка относительного позиционирования, CurrentMousePosExecutorResult не валиден");
                        var currentPos = mousePos.ExecutorResult;
                        var obj = _win.GetObjectFromPoint(currentPos);
                        res = new ScreenShotExecutorResult(ScreenCaptureHelper.GetScreenShot(obj.Pos.X, obj.Pos.Y, obj.Size.WidthX, obj.Size.HeightY));
                    }
                    break;
                case nameof(ObjectExecutorResult):
                    {
                        var mousePos = (ObjectExecutorResult)previousResult;
                        if (mousePos.State != EResultState.Success)
                            throw new Exception("Ошибка относительного позиционирования, ObjectExecutorResult не валиден");
                        var currentPos = mousePos.ExecutorResult;
                        res = new ScreenShotExecutorResult(ScreenCaptureHelper.GetScreenShot(currentPos.Pos.X, currentPos.Pos.Y, currentPos.Size.WidthX, currentPos.Size.HeightY));
                    }
                    break;
                default:
                    break;
            }
            return res ?? previousResult ?? new BaseExecutorResult(EResultState.NoResult);
        }
    }
}
