using System;
using System.Runtime.InteropServices;

namespace Core.Handlers.KeyBoard.DxInput
{
    [StructLayout(LayoutKind.Sequential)]
    struct HARDWAREINPUT
    {
        public Int32 uMsg { get; set; }
        public Int16 wParamL { get; set; }
        public Int16 wParamH { get; set; }
    }
}