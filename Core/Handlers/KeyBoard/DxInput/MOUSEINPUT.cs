using System;
using System.Runtime.InteropServices;

namespace Core.Handlers.KeyBoard.DxInput
{
    [StructLayout(LayoutKind.Sequential)]
    struct MOUSEINPUT
    {
        public Int32 dx { get; set; }
        public Int32 dy { get; set; }
        public Int32 mouseData { get; set; }
        public Int32 dwFlags { get; set; }
        public Int32 time { get; set; }
        public IntPtr dwExtraInfo { get; set; }
    }
}