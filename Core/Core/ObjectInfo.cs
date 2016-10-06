using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    /// <summary>
    /// Базовый класс с информацией об объекте
    /// </summary>
    public class BaseObjectInfo
    {

        /// <summary>
        /// Наименование объекта, если оно есть
        /// </summary>
        [DataMember]
        public String ObjectTitile { get; private set; }
        /// <summary>
        /// Точка указывающая на позицию объекта
        /// </summary>
        [DataMember]
        public Point Pos { get; private set; }

        /// <summary>
        /// Идентификатор дескриптора
        /// </summary>
        [DataMember]
        public IntPtr Descriptor { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor">Дескриптор объекта</param>
        /// <param name="pos">Позация объекта</param>
        /// <param name="objectTitile">Наименование объекта, если оно есть</param>
        public BaseObjectInfo(IntPtr descriptor, Point pos, String objectTitile = null)
        {
            ObjectTitile = objectTitile;
            Descriptor = descriptor;
            Pos = pos;
        }
        public BaseObjectInfo(IntPtr descriptor, Int32 x, Int32 y, String objectTitile = null)
        {
            ObjectTitile = objectTitile;
            Descriptor = descriptor;
            Pos = new Point(x, y);
        }
    }
}
