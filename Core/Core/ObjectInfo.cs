using System;

namespace Core.Core
{
    /// <summary>
    /// Информация об объекте
    /// </summary>
    public class ObjectInfo : BaseObjectInfo
    {
        /// <summary>
        /// Тип объекта
        /// </summary>
        public EObjectType ObjectType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor">Дескриптор объекта</param>
        /// <param name="pos">Позация объекта</param>
        /// <param name="size">Размер объекта</param>
        /// <param name="objectType">Тип объекта</param>
        /// <param name="objectTitile">Наименование объекта, если оно есть</param>
        public ObjectInfo(IntPtr descriptor, Point pos, Size size, EObjectType objectType, String objectTitile = null) : base(descriptor, pos, size, objectTitile)
        {
            ObjectType = objectType;
        }

        public ObjectInfo(IntPtr descriptor, Int32 x, Int32 y, Int32 widthX, Int32 heightY, EObjectType objectType, String objectTitile = null) : base(descriptor, x, y, widthX, heightY, objectTitile)
        {
            ObjectType = objectType;
        }
    }
}
