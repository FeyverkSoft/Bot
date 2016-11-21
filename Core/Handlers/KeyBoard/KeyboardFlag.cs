using System;

namespace Core.Handlers.KeyBoard
{
    /// <summary>
    /// Определяет различные аспекты нажатия клавиши.
    /// </summary>
    [Flags]
    public enum KeyboardFlag : uint
    {
        /// <summary>
        /// KEYEVENTF_EXTENDEDKEY = 0x0001 
        /// (Если он указан, коду сканирования предшествовует префикс байт со значением 0xE0 (224).)
        /// </summary>
        Extendedkey = 0x0001,

        /// <summary>
        /// KEYEVENTF_KEYUP = 0x0002
        /// </summary>
        Keyup = 0x0002,

        /// <summary>
        /// KEYEVENTF_UNICODE = 0x0004 
        /// </summary>
        Unicode = 0x0004,

        /// <summary>
        /// KEYEVENTF_SCANCODE = 0x0008 
        /// </summary>
        Scancode = 0x0008,
    }
}
