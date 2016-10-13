using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Core.Core
{
    /// <summary>
    /// Точка
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// пустая точка
        /// </summary>
        public static readonly Point Empty = new Point();

        /// <summary>
        /// Положение точки по оси X
        /// </summary>
        [DataMember]
        public Int32 X { get; set; }
        /// <summary>
        /// Положение точки по оси Y
        /// </summary>
        [DataMember]
        public Int32 Y { get; set; }

        /// <summary>
        /// Это пустая точка? пустая если x и y = 0
        /// </summary>
        public Boolean IsEmpty => X == 0 && Y == 0;

        [JsonConstructor]
        public Point(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }
        public Point(System.Drawing.Point p)
        {
            X = p.X;
            Y = p.Y;
        }
        /// <summary>
        /// Сместить точку
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Offset(Int32 dx, Int32 dy)
        {
            X += dx;
            Y += dy;
        }

        /// <summary>
        /// преобраховать в System.Drawing.Point
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator System.Drawing.Point(Point p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }
        /// <summary>
        /// Преобразовать System.Drawing.Point в Point
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator Point(System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }

        public override String ToString() => "{X=" + X.ToString(CultureInfo.InvariantCulture) + ",Y=" + Y.ToString(CultureInfo.InvariantCulture) + "}";
    }
}
