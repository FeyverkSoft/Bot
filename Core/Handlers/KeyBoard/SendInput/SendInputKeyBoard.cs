using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Core.Core;
using LogWrapper;

namespace Core.Handlers.KeyBoard.SendInput
{
    public class SendInputKeyBoard : IKeyBoard
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern UInt32 SendInput(UInt32 numberOfInputs, INPUT[] inputs, Int32 sizeOfInputStructure);

        private void SimulateKeyDown(KeyName keyCode)
        {
            var down = new INPUT
            {
                Type = (UInt32)1,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk =  keyCode.GetVKeyCode(),
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

        private void SimulateKeyUp(KeyName keyCode)
        {
            var up = new INPUT
            {
                Type = (UInt32)1,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = keyCode.GetVKeyCode(),
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

        private void SimulateKeyPress(KeyName keyCode)
        {
            var down = new INPUT
            {
                Type = (UInt32)1,
                Data =
                {
                    Keyboard = new KEYBDINPUT
                    {
                        Vk = keyCode.GetVKeyCode(),
                        Scan =  0,
                        Flags = 0,
                        Time = 200,
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
                        Vk =  keyCode.GetVKeyCode(),
                        Scan =  0,
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
        public void PressKey(KeyName key, UInt32 pressTime = 0)
        {
            Log.WriteLine($"-- PressKey -- {GetType().Name}.{nameof(PressKeys)}; key:{key}; pressTime: {pressTime}");
            SimulateKeyPress(key);
        }

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        public void PressKeys(List<KeyName> list)
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
                    SimulateKeyDown(key);
                    break;
                case KeyAction.Up:
                    SimulateKeyUp(key);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}
