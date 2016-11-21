using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Core.Core;
using LogWrapper;

namespace Core.Handlers
{
    public class SendInputKeyBoard : IKeyBoard
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern UInt32 SendInput(UInt32 numberOfInputs, INPUT[] inputs, Int32 sizeOfInputStructure);

        private void SimulateKeyDown(KeyCode keyCode)
        {
            var down = new INPUT
            {
                Type = (UInt32)1,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = (UInt16) keyCode,
                        Scan = 0,
                        Flags = 0,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };

            var inputList = new INPUT[] { down };

            var numberOfSuccessfulSimulatedInputs = SendInput(1, inputList, Marshal.SizeOf(typeof(INPUT)));
            if (numberOfSuccessfulSimulatedInputs == 0) throw new Exception(
                $"The key down simulation for {keyCode} was not successful.");
        }

        private void SimulateKeyUp(KeyCode keyCode)
        {
            var up = new INPUT
            {
                Type = (UInt32)1,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = (UInt16) keyCode,
                        Scan = 0,
                        Flags = (UInt32) KeyboardFlag.Keyup,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };

            var inputList = new INPUT[] { up };

            var numberOfSuccessfulSimulatedInputs = SendInput(1, inputList, Marshal.SizeOf(typeof(INPUT)));
            if (numberOfSuccessfulSimulatedInputs == 0) throw new Exception(
                $"The key up simulation for {keyCode} was not successful.");
        }

        private void SimulateKeyPress(KeyCode keyCode)
        {
            var down = new INPUT
            {
                Type = (UInt32)1,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = (UInt16) keyCode,
                        Scan = 0,
                        Flags = 0,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };

            var up = new INPUT
            {
                Type = (UInt32)1,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = (UInt16) keyCode,
                        Scan = 0,
                        Flags = (UInt32) KeyboardFlag.Keyup,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };

            var inputList = new INPUT[] { down, up };

            var numberOfSuccessfulSimulatedInputs = SendInput(2, inputList, Marshal.SizeOf(typeof(INPUT)));
            if (numberOfSuccessfulSimulatedInputs == 0) throw new Exception(
                $"The key press simulation for {keyCode} was not successful.");
        }


        /// <summary>
        /// Эмулировать нажатие клавишы на клавиатуре
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressTime"></param>
        public void PressKey(KeyCode key, UInt32 pressTime = 0)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            SimulateKeyPress(key);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        public void PressKeys(List<KeyCode> list)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(PressKeys)}");
            foreach (var k in list) //Выполняем событие последовательного нажатия несколькоих клавиш
            {
                SimulateKeyDown(k);
            }
            foreach (var k in list) //Выполняем событие последовательного отпускания несколькоих клавиш
            {
                SimulateKeyUp(k);
            }
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(PressKeys)}");
        }
    }
}
