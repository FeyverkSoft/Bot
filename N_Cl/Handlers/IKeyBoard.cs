using System.Collections.Generic;
using Executor.Core;

namespace Executor.Handlers
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
        void PressKey(KeyCode key);

        /// <summary>
        /// Эмулирует нажатие нескольких клавиш
        /// </summary>
        void PressKeys(List<KeyCode> list);
    }
}