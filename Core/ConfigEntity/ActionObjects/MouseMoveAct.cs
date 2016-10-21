using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Helpers;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие смещения мышки
    /// </summary>
    [DataContract]
    [Description("Событие смещения мышки")]
    public class MouseMoveAct : IAction
    {
        /// <summary>
        /// Смещение по оси X
        /// </summary>
        [Description("Смещение по оси X")]
        [DataMember]
        public Int32 Dx { get; protected set; } = 0;
        /// <summary>
        /// Смещение по оси Y
        /// </summary>
        [DataMember]
        [Description("Смещение по оси Y")]
        public Int32 Dy { get; protected set; } = 0;
        /// <summary>
        /// Двигать к объекту являющемуся результатом предыдущего вызова, если это возможно
        /// </summary>
        [DataMember]
        [Description("Двигать к объекту являющемуся результатом предыдущего вызова, если это возможно")]
        public Boolean ToObject { get; protected set; }

        public MouseMoveAct(Boolean toObject = true)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(toObject:{toObject})");
            ToObject = toObject;
        }

        [JsonConstructor]
        public MouseMoveAct(Int32 dx, Int32 dy, Boolean toObject = false)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(dx:{dx}; dy: {dy}); toObject:{toObject};");
            Dx = dx;
            Dy = dy;
            ToObject = toObject;
        }

        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return this.GetString();
        }
    }
}
