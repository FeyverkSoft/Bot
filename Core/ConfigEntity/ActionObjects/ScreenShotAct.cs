using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Core;
using Core.Helpers;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие получения скриншота
    /// </summary>
    [Description("Действие получения скриншота")]
    [DataContract]
    public class ScreenShotAct : IAction
    {

        /// <summary>
        /// Начало прямоугольной области которой надо сделать скриншот
        /// </summary>
        [Description("Начало прямоугольной области которой надо сделать скриншот")]
        public Point Point { get; private set; } = Point.Empty;

        /// <summary>
        /// Размер прямоугольной области
        /// </summary>
        [Description("Размер прямоугольной области")]
        public Size Size { get; private set; } = Point.Empty;
        /// <summary>
        /// Описание пути и параметров файла для сейва
        /// </summary>
        [Description("Описание пути и параметров файла для сейва")]
        public SaveFileParam SaveFileParam { get; private set; }

        /// <summary>
        /// Сделать картинку в оттенках серого?
        /// </summary>
        [Description("Сделать картинку в оттенках серого?")]
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
        [JsonConstructor]
        public ScreenShotAct(Point point, Size size, SaveFileParam saveFileParam, Boolean grayScale = false) :this(saveFileParam, grayScale)
        {
            Point = point;
            Size = size;
        }

        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return this.GetString();
        }
    }
}
