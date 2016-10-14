using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Событие установки позиции мышки в указанную точку
    /// </summary>
    [Description("Событие установки позиции мышки в указанную точку")]
    [DataContract]
    public class MouseSetPosAct : IAction
    {
        Int32 x = 0, y = 0;
        /// <summary>
        /// Положение указателя по оси X
        /// </summary>
        [Description("Положение указателя по оси X")]
        [DataMember]
        public Int32 X
        {
            get { return x; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(X));
                x = value;
            }
        }
        /// <summary>
        /// Положение указателя по оси Y
        /// </summary>
        [DataMember]
        [Description("Положение указателя по оси Y")]
        public Int32 Y
        {
            get { return y; }
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Y));
                y = value;
            }
        }
        /// <summary>
        /// Название окна относительно которого устанавливается позиция
        /// </summary>
        [DataMember]
        [Description("Название окна относительно которого устанавливается позиция")]
        public String RelativelyWindowName { get; private set; }

        /// <summary>
        /// Использовать относительную позицию или нет
        /// </summary>
        [DataMember]
        [Description("Использовать относительную позицию или нет")]
        public Boolean Relatively { get; private set; }
        [JsonConstructor]
        public MouseSetPosAct(Int32 x, Int32 y, Boolean relatively = false, String relativelyWindowName = null)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(x:{x}; y: {y}; relativelyWindowName: {relativelyWindowName ?? ""}; relatively:{relatively};)");
            X = x;
            Y = y;
            RelativelyWindowName = relativelyWindowName;
            Relatively = relatively;
        }
    }
}
