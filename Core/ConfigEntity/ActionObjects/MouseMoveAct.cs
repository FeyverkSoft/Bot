using System;
using System.Runtime.Serialization;
using Core.Attributes;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие смещения мышки
    /// </summary>
    [DataContract]
    [LocDescription("MouseMoveAct")]
    public class MouseMoveAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.MouseMove;


        /// <summary>
        /// Смещение по оси X
        /// </summary>
        [LocDescription("MouseMoveAct_Dx")]
        [DataMember]
        public Int32 Dx { get; set; } = 0;
        /// <summary>
        /// Смещение по оси Y
        /// </summary>
        [DataMember]
        [LocDescription("MouseMoveAct_Dy")]
        public Int32 Dy { get; set; } = 0;
        /// <summary>
        /// Двигать к объекту являющемуся результатом предыдущего вызова, если это возможно
        /// </summary>
        [DataMember]
        [LocDescription("MouseMoveAct_ToObject")]
        public Boolean ToObject { get; set; }

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

        public MouseMoveAct(){}
    }
}
