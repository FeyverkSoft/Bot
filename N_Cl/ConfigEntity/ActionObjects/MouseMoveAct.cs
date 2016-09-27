using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Executor.ConfigEntity.ActionObjects
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

        [JsonConstructor]
        public MouseMoveAct(Int32 dx, Int32 dy)
        {
            Debug.WriteLine($"{GetType().Name}.ctor->(dx:{dx}; dy: {dy})");
            Dx = dx;
            Dy = dy;
        }
    }
}
