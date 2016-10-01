using System;
using System.Runtime.Serialization;
using LogWrapper;
using Newtonsoft.Json;

namespace Core.ConfigEntity.ActionObjects
{
    [DataContract]
    public class MouseSetPosAct : IAction
    {
        Int32 x = 0, y = 0;
        /// <summary>
        /// Положение указателя по оси X
        /// </summary>
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
        public String RelativelyWindowName { get; private set; }

        [JsonConstructor]
        public MouseSetPosAct(Int32 x, Int32 y, String relativelyWindowName = null)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(x:{x}; y: {y}; relativelyWindowName: {relativelyWindowName ?? ""})");
            X = x;
            Y = y;
            RelativelyWindowName = relativelyWindowName;
        }
    }
}
