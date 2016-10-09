using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Core.Handlers;

namespace Core.Helpers
{
    internal static class ScreenCaptureHelper
    {
        /// <summary>
        /// Возвращает скриншот экрана указанной области
        /// </summary>
        /// <param name="x">начальная точка по координате Х</param>
        /// <param name="y">начальная точка по коорденате Y</param>
        /// <param name="width">Шрина области</param>
        /// <param name="height">Высота области</param>
        /// <returns></returns>
        public static Bitmap GetScreenShot(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            var primaryScreen = Screen.PrimaryScreen;
            var bmpScreenShot = new Bitmap(width, height);
            Graphics gScreenShot = Graphics.FromImage(bmpScreenShot);
            gScreenShot.CopyFromScreen(x, y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            return bmpScreenShot;
        }

    }
}
