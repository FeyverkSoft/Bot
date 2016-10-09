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

        public ScreenShotAct(SaveFileParam saveFileParam)
        {
            SaveFileParam = saveFileParam;
        }
        public ScreenShotAct(Point point, Size size, SaveFileParam saveFileParam):this(saveFileParam)
        {
            Point = point;
            Size = size;
        }

    }
}
