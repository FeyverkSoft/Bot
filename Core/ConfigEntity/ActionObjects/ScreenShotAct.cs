using System;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Core;
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
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.GetScreenshot;

        /// <summary>
        /// Начало прямоугольной области которой надо сделать скриншот
        /// </summary>
        [LocDescription("ScreenShotAct_Point")]
        [DataMember]
        public Point Point { get; set; } = Point.Empty;

        /// <summary>
        /// Размер прямоугольной области
        /// </summary>
        [LocDescription("ScreenShotAct_Size")]
        [DataMember]
        public Size Size { get; set; } = Point.Empty;
        /// <summary>
        /// Описание пути и параметров файла для сейва
        /// </summary>
        [LocDescription("ScreenShotAct_SaveFileParam")]
        [DataMember]
        public SaveFileParam SaveFileParam { get; set; }

        /// <summary>
        /// Сделать картинку в оттенках серого?
        /// </summary>
        [LocDescription("ScreenShotAct_GrayScale")]
        [DataMember]
        public Boolean GrayScale { get; set; }


        /// <summary>
        /// Двигать к объекту являющемуся результатом предыдущего вызова, если это возможно
        /// </summary>
        [DataMember]
        [LocDescription("ScreenShotAct_PrevResult")]
        public Boolean PrevResult { get; set; } = false;


        public ScreenShotAct(Boolean grayScale = false)
        {
            SaveFileParam = new SaveFileParam("ScreenShot", ImageFileFormat.png);
            GrayScale = grayScale;
        }

        /// <param name="grayScale">Преобразовать в градации серого</param>
        /// <param name="prevResult">Использовать для позиции результат предыдущего действия</param>
        /// <param name="saveFileParam">Параметры сохранения файла</param>
        public ScreenShotAct(SaveFileParam saveFileParam, Boolean grayScale = false, Boolean prevResult = false)
        {
            SaveFileParam = saveFileParam;
            GrayScale = grayScale;
            PrevResult = prevResult;
        }
        [JsonConstructor]
        public ScreenShotAct(Point point, Size size, SaveFileParam saveFileParam, Boolean grayScale = false) :this(saveFileParam, grayScale)
        {
            Point = point;
            Size = size;
        }

        public ScreenShotAct(){}
    }
}
