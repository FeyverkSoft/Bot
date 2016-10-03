using System;
using System.Runtime.Serialization;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    [DataContract]
    public class MouseMoveAct : IAction
    {
        /// <summary>
        /// Смещение по оси X
        /// </summary>
        [DataMember]
        public Int32 Dx { get; set; } = 0;
        /// <summary>
        /// Смещение по оси Y
        /// </summary>
        [DataMember]
        public Int32 Dy { get; set; } = 0;
        /// <summary>
        /// Двигать к объекту являющемуся результатом предыдущего вызова, если это возможно
        /// </summary>
        [DataMember]
        public Boolean ToObject { get; set; }
        [JsonConstructor]
        public MouseMoveAct(Int32 dx, Int32 dy)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(dx:{dx}; dy: {dy})");
            Dx = dx;
            Dy = dy;
        }

        [JsonConstructor]
        public MouseMoveAct(Boolean toObject = true)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(toObject:{toObject})");
            ToObject = toObject;
        }

        [JsonConstructor]
        public MouseMoveAct(Int32 dx, Int32 dy, Boolean toObject)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(dx:{dx}; dy: {dy}); toObject:{toObject};");
            Dx = dx;
            Dy = dy;
            ToObject = toObject;
        }
    }
}
