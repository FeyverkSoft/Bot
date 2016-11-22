﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Core.Core;
using LogWrapper;

namespace Core.Handlers.KeyBoard.Native
{
    /// <summary>
    /// Класс для эмуляции клавиатуры
    /// </summary>
    public class NativeKeyBoard : IKeyBoard
    {

        [DllImport("user32.dll")]
        static extern void keybd_event(UInt16 bVk, Byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        private void PressKeyInternal(KeyName keyCode, UInt32 pressTime = 0)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeyInternal)}");
            Log.WriteLine($"--> press: 0x{keyCode}, ");
            keybd_event(keyCode.GetVKeyCode(), 0x45, (UInt32)KeyboardFlag.Extendedkey | 0, (IntPtr)0);
            //Thread.Sleep((Int32)pressTime);
            keybd_event(keyCode.GetVKeyCode(), 0x45, (UInt32)(KeyboardFlag.Extendedkey | KeyboardFlag.Keyup), (IntPtr)0);
            Log.WriteLine($"--> up: 0x{keyCode}");
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeyInternal)}");
        }

        /// <summary>
        /// Эмулировать нажатие клавишы на клавиатуре
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressTime"></param>
        public void PressKey(KeyName key, UInt32 pressTime = 0)
        {
            PressKeyInternal(key, pressTime);
        }

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        public void PressKeys(List<KeyName> list)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            foreach (var k in list) //Выполняем событие последовательного нажатия несколькоих клавиш
            {
                Log.Write($"--> press: {k}, ");
                keybd_event(k.GetVKeyCode(), 0, 0, (IntPtr)0);
            }
            foreach (var k in list) //Выполняем событие последовательного отпускания несколькоих клавиш
            {
                Log.Write($"--> up: {k}, ");
                keybd_event(k.GetVKeyCode(), 0, (UInt32)KeyboardFlag.Keyup, (IntPtr)0);
            }
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }
    }
}
