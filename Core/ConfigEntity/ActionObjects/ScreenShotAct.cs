using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;
using Core.Helpers;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Действие получения скриншота
    /// </summary>
    [LocDescription("ScreenShotAct")]
    [DataContract]
    public class ScreenShotAct : BaseActionObject
    {

        /// <summary>
        /// Начало прямоугольной области которой надо сделать скриншот
        /// </summary>
        [LocDescription("ScreenShotAct_Point")]
        public Point Point { get; private set; } = Point.Empty;

        /// <summary>
        /// Размер прямоугольной области
        /// </summary>
        [LocDescription("ScreenShotAct_Size")]
        public Size Size { get; private set; } = Point.Empty;
        /// <summary>
        /// Описание пути и параметров файла для сейва
        /// </summary>
        [LocDescription("ScreenShotAct_SaveFileParam")]
        public SaveFileParam SaveFileParam { get; private set; }

        /// <summary>
        /// Сделать картинку в оттенках серого?
        /// </summary>
        [LocDescription("ScreenShotAct_GrayScale")]
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
    }
}
