using System;
using System.Collections.Generic;
using Core.Core;

namespace Core.Handlers
{
    /// <summary>
    /// Интерфейс Класса для эмуляции клавиатуры
    /// </summary>
    public interface IKeyBoard
    {
        /// <summary>
        /// Эмулировать нажатие клавишы на клавиатуре
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressTime"></param>
        void PressKey(KeyCode key, UInt32 pressTime = 0);

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        void PressKeys(List<KeyCode> list);
    }
}