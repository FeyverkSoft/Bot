﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Core.Core;
using LogWrapper;

namespace Core.Handlers.KeyBoard.DxInput
{
    public class DxInputKeyBoard : IKeyBoard
    {
        private readonly Random _rand = new Random();
        [DllImport("user32.dll")]
        static extern UInt32 SendInput(UInt32 nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] pInputs, Int32 cbSize);

        private void SendKey(KeyName keycode, Int32 keyUporDown)
        {
            var inputData = new INPUT[]
            {
                new INPUT
                {
                    type = 1,
                    ki = new KEYBDINPUT
                    {
                        wScan = keycode.GetDxCode(),
                        dwFlags = keyUporDown,
                        time = 50,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
            SendInput(1, inputData, Marshal.SizeOf(typeof(INPUT)));
        }


        /// <summary>
        /// Эмулировать нажатие клавишы на клавиатуре
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressTime"></param>
        public void PressKey(KeyName key, UInt32 pressTime = 0)
        {
            Log.WriteLine($"-- PressKey -- {GetType().Name}.{nameof(PressKeys)}; key: {key}; pressTime:{pressTime}");
            SendKey(key, (Int32)KeyboardFlag.Scancode);
            Thread.Sleep((Int32) (pressTime + _rand.Next(25)));//дичь
            SendKey(key, (Int32)(KeyboardFlag.Keyup | KeyboardFlag.Scancode));
        }

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        public void PressKeys(List<KeyName> list)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            foreach (var k in list) //Выполняем событие последовательного нажатия несколькоих клавиш
            {
                SendKey(k, (Int32)KeyboardFlag.Scancode);
            }
            foreach (var k in list) //Выполняем событие последовательного отпускания несколькоих клавиш
            {
                SendKey(k, (Int32)(KeyboardFlag.Keyup | KeyboardFlag.Scancode));
            }
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }

        /// <summary>
        /// Вызвать событие для клавиши
        /// </summary>
        /// <param name="key">Клавиша</param>
        /// <param name="action">Событие</param>
        public void InvokeKeyAct(KeyName key, KeyAction action)
        {
            Log.WriteLine($"-- InvokeKeyAct -- {GetType().Name}.{nameof(PressKeys)}, key:{key}; action:{action}");
            switch (action)
            {
                case KeyAction.Press:
                    PressKey(key);
                    break;
                case KeyAction.Down:
                    SendKey(key, (Int32)KeyboardFlag.Scancode);
                    break;
                case KeyAction.Up:
                    SendKey(key, (Int32)(KeyboardFlag.Keyup | KeyboardFlag.Scancode));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}
