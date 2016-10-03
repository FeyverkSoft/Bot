using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    /// <summary>
    /// Информация о текущем положении указателя
    /// </summary>
    public class CurrentMousePos
    {
        /// <summary>
        /// Положение указателя по оси X
        /// </summary>
        public Int32 X { get; }
        /// <summary>
        /// Положение указателя по оси Y
        /// </summary>
        public Int32 Y { get; }

        public CurrentMousePos(Int32 x, Int32 y)
        {
            X = x;
            Y = y;
        }
    }
}
