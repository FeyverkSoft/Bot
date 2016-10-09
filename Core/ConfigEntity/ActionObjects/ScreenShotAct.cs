using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие получения скриншота
    /// </summary>
    public class ScreenShotAct : IAction
    {

        /// <summary>
        /// Начало прямоугольной области которой надо сделать скриншёт
        /// </summary>
        public Point Point { get; private set; } = Point.Empty;

        /// <summary>
        /// Размер прямоугольной области
        /// </summary>
        public Size Size { get; private set; } = Point.Empty;
        /// <summary>
        /// Описание пути и параметров файла для сейва
        /// </summary>
        public SaveFileParam SaveFileParam { get; private set; }

        /// <summary>
        /// Сделать картинку в оттенках серого?
        /// </summary>
        public Boolean GrayScale { get; private set; }

        public ScreenShotAct(Boolean grayScale = false)
        {
            SaveFileParam = new SaveFileParam("ScreenShot","png");
            GrayScale = grayScale;
        }

        public ScreenShotAct(SaveFileParam saveFileParam, Boolean grayScale = false)
        {
            SaveFileParam = saveFileParam;
            GrayScale = grayScale;
        }
        public ScreenShotAct(Point point, Size size, SaveFileParam saveFileParam, Boolean grayScale = false) :this(saveFileParam, grayScale)
        {
            Point = point;
            Size = size;
        }

    }
}
