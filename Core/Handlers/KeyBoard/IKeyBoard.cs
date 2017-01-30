using System;
using System.Collections.Generic;
using Core.Core;

namespace Core.Handlers.KeyBoard
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
        void PressKey(KeyName key, UInt32 pressTime = 0);

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        void PressKeys(List<KeyName> list);

        /// <summary>
        /// Вызвать событие для клавиши
        /// </summary>
        /// <param name="key">Клавиша</param>
        /// <param name="action">Событие</param>
        void InvokeKeyAct(KeyName key, KeyAction action);
    }
}