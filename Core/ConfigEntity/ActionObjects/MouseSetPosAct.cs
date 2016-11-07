using System;
using System.Runtime.Serialization;
using CommonLib.Attributes;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие установки позиции мышки в указанную точку
    /// </summary>
    [LocDescription("MouseSetPosAct", typeof(Resources.CoreText))]
    [DataContract]
    public class MouseSetPosAct : BaseActionObject
    {
        /// <summary>
        /// Тип действия для внутренней фабрики
        /// </summary>
        [IgnoreDataMember]
        public new static ActionType ActionType => ActionType.MouseSetPos;

        Int32 _x = 0, _y = 0;
        /// <summary>
        /// Положение указателя по оси X
        /// </summary>
        [LocDescription("MouseSetPosAct_X", typeof(Resources.CoreText))]
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
        [LocDescription("MouseSetPosAct_Y", typeof(Resources.CoreText))]
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
        [LocDescription("MouseSetPosAct_RelativelyWindowName", typeof(Resources.CoreText))]
        public String RelativelyWindowName { get; set; }

        /// <summary>
        /// Использовать относительную позицию или нет
        /// </summary>
        [DataMember]
        [LocDescription("MouseSetPosAct_Relatively", typeof(Resources.CoreText))]
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
