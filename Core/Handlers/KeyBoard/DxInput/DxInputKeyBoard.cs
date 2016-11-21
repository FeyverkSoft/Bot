using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Core.Core;
using LogWrapper;

namespace Core.Handlers.KeyBoard.DxInput
{
    public class DxInputKeyBoard : IKeyBoard
    {
        Random r = new Random();
        [DllImport("user32.dll")]
        static extern UInt32 SendInput(UInt32 nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] pInputs, Int32 cbSize);


        public void Send_Key(Int16 Keycode, Int32 KeyUporDown)
        {
            INPUT[] inputData = new INPUT[1];

            inputData[0].type = 1;
            inputData[0].ki.wScan = Keycode;
            inputData[0].ki.dwFlags = KeyUporDown;
            inputData[0].ki.time = 100;
            inputData[0].ki.dwExtraInfo = IntPtr.Zero;

            SendInput(1, inputData, Marshal.SizeOf(typeof(INPUT)));
        }


        /// <summary>
        /// Эмулировать нажатие клавишы на клавиатуре
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressTime"></param>
        public void PressKey(KeyCode key, UInt32 pressTime = 0)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            Send_Key(0x11, (Int32)KeyboardFlag.Scancode);
            Thread.Sleep((Int32)pressTime + r.Next(50));
            Send_Key(0x11, (Int32)(KeyboardFlag.Keyup | KeyboardFlag.Scancode));
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        public void PressKeys(List<KeyCode> list)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            /* foreach (var k in list) //Выполняем событие последовательного нажатия несколькоих клавиш
             {
                 SimulateKeyDown(k);
             }
             foreach (var k in list) //Выполняем событие последовательного отпускания несколькоих клавиш
             {
                 SimulateKeyUp(k);
             }*/
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }
    }
}
