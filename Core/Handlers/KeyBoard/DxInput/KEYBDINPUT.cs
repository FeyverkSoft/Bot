using System;
using System.Runtime.InteropServices;

namespace Core.Handlers.KeyBoard.DxInput
{
    [StructLayout(LayoutKind.Sequential)]
    struct KEYBDINPUT
    {
        public UInt16 wVk { get; set; }
        public UInt16 wScan { get; set; }
        public Int32 dwFlags { get; set; }
        public Int32 time { get; set; }
        public IntPtr dwExtraInfo { get; set; }
    }
}