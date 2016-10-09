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
        /// <param name="grayScale">Вернуть в оттенках серого?</param>
        /// <returns></returns>
        public static Bitmap GetScreenShot(Int32 x, Int32 y, Int32 width, Int32 height, Boolean grayScale = false)
        {
            //var primaryScreen = Screen.PrimaryScreen;
            var bmpScreenShot = new Bitmap(width, height);
            Graphics gScreenShot = Graphics.FromImage(bmpScreenShot);
            gScreenShot.CopyFromScreen(x, y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            return grayScale ? GrayScale(bmpScreenShot) : bmpScreenShot;
        }

        private static Bitmap GrayScale(Bitmap imageBitmap)
        {
            for (var x = 0; x < imageBitmap.Width; x++)
            {
                for (var y = 0; y < imageBitmap.Height; y++)
                {
                    Color bitmapColor = imageBitmap.GetPixel(x, y);
                    var colorGray = (int)(bitmapColor.R * 0.299 +
                                           bitmapColor.G * 0.587 + bitmapColor.B * 0.114);
                    imageBitmap.SetPixel(x, y, Color.FromArgb(colorGray, colorGray, colorGray));
                }
            }
            return imageBitmap;
        }
    }
}
