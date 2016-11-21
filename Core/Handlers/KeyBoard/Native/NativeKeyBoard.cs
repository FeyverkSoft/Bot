using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Core.Core;
using Core.Handlers.KeyBoard.SendInput;
using LogWrapper;

namespace Core.Handlers.KeyBoard.Native
{
    /// <summary>
    /// Класс для эмуляции клавиатуры
    /// </summary>
    public class NativeKeyBoard : IKeyBoard
    {

        [DllImport("user32.dll")]
        static extern void keybd_event(Byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        private void PressKeyInternal(Byte keyCode, UInt32 pressTime = 0)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeyInternal)}");
            Log.WriteLine($"--> press: 0x{keyCode:X2}, ");
            keybd_event(keyCode, 0x45, (uint)KeyboardFlag.Extendedkey | 0, (IntPtr)0);
            //Thread.Sleep((Int32)pressTime);
            keybd_event(keyCode, 0x45, (uint)(KeyboardFlag.Extendedkey | KeyboardFlag.Keyup), (IntPtr)0);
            Log.WriteLine($"--> up: 0x{keyCode:X2}");
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeyInternal)}");
        }

        /// <summary>
        /// Эмулировать нажатие клавишы на клавиатуре
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressTime"></param>
        public void PressKey(KeyCode key, UInt32 pressTime = 0)
        {
            PressKeyInternal((Byte)key, pressTime);
        }

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        public void PressKeys(List<KeyCode> list)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            foreach (var k in list) //Выполняем событие последовательного нажатия несколькоих клавиш
            {
                Log.Write($"--> press: 0x{((Int32)k):X2}, ");
                keybd_event((Byte)k, 0, 0, (IntPtr)0);
            }
            foreach (var k in list) //Выполняем событие последовательного отпускания несколькоих клавиш
            {
                Log.Write($"--> up: 0x{((Int32)k):X2}, ");
                keybd_event((Byte)k, 0, (uint)KeyboardFlag.Keyup, (IntPtr)0);
            }
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }
    }
}
