using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Core.Core
{
    /// <summary>
    /// Точка
    /// </summary>
    public class Size
    {
        /// <summary>
        /// пустая точка
        /// </summary>
        public static readonly Size Empty = new Size();

        /// <summary>
        /// Размер по оси X, Ширина
        /// </summary>
        [DataMember]
        public Int32 WidthX { get; set; }
        /// <summary>
        /// Размер по оси Y, Высота
        /// </summary>
        [DataMember]
        public Int32 HeightY { get; set; }

        /// <summary>
        /// Это пустой размер? пустая если x и y = 0
        /// </summary>
        public Boolean IsEmpty => WidthX == 0 && HeightY == 0;

        public Size(Int32 x, Int32 y)
        {
            WidthX = x;
            HeightY = y;
        }
        public Size(System.Drawing.Point p)
        {
            WidthX = p.X;
            HeightY = p.Y;
        }
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Add(Int32 dx, Int32 dy)
        {
            WidthX += dx;
            HeightY += dy;
        }

        /// <summary>
        /// преобразовать в Size
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator Size(Point p)
        {
            return new Size(p.X, p.Y);
        }
        /// <summary>
        /// Преобразовать ( в Point
        /// </summary>
        /// <param name="s"></param>
        public static implicit operator Point(Size s)
        {
            return new Point(s.WidthX, s.HeightY);
        }

        public override String ToString() => "{X=" + WidthX.ToString(CultureInfo.InvariantCulture) + ",Y=" + HeightY.ToString(CultureInfo.InvariantCulture) + "}";

        public Size()
        {
            
        }
    }
}
