using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogWrapper
{
    [Flags]
    public enum LogLevel
    {
        Info = 2,
        Warning = 4,
        Error = 8
    }
}
