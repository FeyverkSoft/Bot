using System;
using System.Runtime.Serialization;
using Core.Attributes;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие установки позиции мышки в указанную точку
    /// </summary>
    [LocDescription("MouseSetPosAct")]
    [DataContract]
    public class MouseSetPosAct : BaseActionObject
    {
        Int32 _x = 0, _y = 0;
        /// <summary>
        /// Положение указателя по оси X
        /// </summary>
        [LocDescription("MouseSetPosAct_X")]
        [DataMember]
        public Int32 X
        {
            get { return _x; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(X));
                _x = value;
            }
        }
        /// <summary>
        /// Положение указателя по оси Y
        /// </summary>
        [DataMember]
        [LocDescription("MouseSetPosAct_Y")]
        public Int32 Y
        {
            get { return _y; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Y));
                _y = value;
            }
        }
        /// <summary>
        /// Название окна относительно которого устанавливается позиция
        /// </summary>
        [DataMember]
        [LocDescription("MouseSetPosAct_RelativelyWindowName")]
        public String RelativelyWindowName { get; set; }

        /// <summary>
        /// Использовать относительную позицию или нет
        /// </summary>
        [DataMember]
        [LocDescription("MouseSetPosAct_Relatively")]
        public Boolean Relatively { get; set; }
        [JsonConstructor]
        public MouseSetPosAct(Int32 x, Int32 y, Boolean relatively = false, String relativelyWindowName = null)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(x:{x}; y: {y}; relativelyWindowName: {relativelyWindowName ?? ""}; relatively:{relatively};)");
            X = x;
            Y = y;
            RelativelyWindowName = relativelyWindowName;
            Relatively = relatively;
        }
        public MouseSetPosAct() { }
    }
}
