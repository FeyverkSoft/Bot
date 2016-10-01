using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Core.Core;

namespace Core.Handlers
{
    /// <summary>
    /// Класс для эмуляции клавиатуры
    /// </summary>
    public class KeyBoard : IKeyBoard
    {
        const Int32 KEYEVENTF_EXTENDEDKEY = 0x1;
        const Int32 KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll")]
        static extern void keybd_event(Int32 bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private void PressKeyInternal(Int32 keyCode)
        {
            Debug.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeyInternal)}");
            Debug.WriteLine($"--> press: 0x{keyCode.ToString("X2")}, ");
            keybd_event(keyCode, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(keyCode, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Debug.WriteLine($"--> up: 0x{keyCode.ToString("X2")}");
            Debug.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeyInternal)}");
        }

        /// <summary>
        /// Эмулировать нажатие клавишы на клавиатуре
        /// </summary>
        /// <param name="key"></param>
        public void PressKey(KeyCode key)
        {
            PressKeyInternal((Int32)key);
        }

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        public void PressKeys(List<KeyCode> list)
        {
            Debug.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            foreach (var k in list) //Выполняем событие последовательного нажатия несколькоих клавиш
            {
                Debug.Write($"--> press: 0x{((Int32)k).ToString("X2")}, ");
                keybd_event((Int32)k, 0, 0, 0);
            }
            foreach (var k in list) //Выполняем событие последовательного отпускания несколькоих клавиш
            {
                Debug.Write($"--> up: 0x{((Int32)k).ToString("X2")}, ");
                keybd_event((Int32)k, 0, KEYEVENTF_KEYUP, 0);
            }
            Debug.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }
    }
}
