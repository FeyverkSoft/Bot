using System;
using System.Drawing;
using System.Linq;
using Core.ActionExecutors.ExecutorResult;
using Core.ConfigEntity;
using DNPlugin.ActionObjects;
using DNPlugin.Helpers;
using Plugin;

namespace DNPlugin
{
    public class PluginCore : IPlugin
    {
        public String Name => "ImageComparer";

        /// <summary>
        /// Вызвать выполнение действия у указанной фfбрики
        /// </summary>
        /// <param name="actions">Список действи которые должен выполнить исполнитель</param>
        /// <param name="previousResult">Результат выполнения предыдущего действия, не обязательно</param>
        /// <returns></returns>
        public IExecutorResult Invoke(BotAction actions, IExecutorResult previousResult = null)
        {
            if (actions?.ActionType != ActionType.PluginAct)
                throw new NotSupportedException($"ActionType is not PluginAct");
            if (!(previousResult is BitmapExecutorResult))
                throw new NotSupportedException($"{nameof(previousResult)} is not {nameof(BitmapExecutorResult)}");
            var act = actions.SubActions.Cast<PluginCompareImgAction>().First();

            var img1 = ((BitmapExecutorResult)previousResult).Bitmap;
            var img2 = (Bitmap)Image.FromFile(act.SamplePath);
            //2) - resize
            var w = (img1.Width + img2.Width) / 2;
            var h = (img1.Height + img2.Height) / 2;
            img1 = ImgHelpers.ResizeImg(img1, w, h);
            img2 = ImgHelpers.ResizeImg(img2, w, h);
            //3) GrayScale
            var byteImg1 = img1.GrayScale();
            var byteImg2 = img2.GrayScale();
            img1.Dispose();
            img2.Dispose();
            //4) Сhunked
            byteImg1.Chunked(6);
            byteImg2.Chunked(6);
            //5) Dev
            var devRes = ImgHelpers.Dev(byteImg1, byteImg2);
            //6) Threshold
            devRes.Threshold();
            //7) Сhunked
            devRes.Chunked();
            //8) Threshold
            devRes.Threshold(4);
            
            var s = devRes.GetMaxIslandSize();
            var procent = (s*100)/(w*h);
            if (procent > act.Procent)
                return new BooleanExecutorResult(true);
            return new BooleanExecutorResult(false);
        }

    /// <summary>
    /// Вызвать выполнение действия у указанной фабрики
    /// </summary>
    /// <param name="previousResult">Результат выполнения предыдущего действия, (не обязательно :))</param>
    /// <returns></returns>
    public IExecutorResult Invoke(IExecutorResult previousResult = null)
    {
        throw new NotSupportedException();
    }
}
}
