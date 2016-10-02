using System;

namespace Core.Core
{
    /// <summary>
    /// Класс содержащий информацию об 
    /// </summary>
    public class WinInfo
    {
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public String WindowTitile { get; private set; }
        /// <summary>
        /// Позиция окна по оси X
        /// </summary>
        public Int32 PosX { get; private set; } = 0;
        /// <summary>
        /// Позиция окна по оси Y
        /// </summary>
        public Int32 PosY { get; private set; } = 0;
        /// <summary>
        /// Было ли окно найдено?
        /// </summary>
        public Boolean IsFound { get; private set; } = false;
        /// <summary>
        /// Идентификатор дескриптора окна
        /// </summary>
        public IntPtr Descriptor { get; private set; }
        public WinInfo(String title, Int32 posX, Int32 posY, IntPtr descriptor, Boolean isFound = true)
        {
            WindowTitile = title;
            PosX = posX;
            PosY = posY;
            IsFound = isFound;
            Descriptor = descriptor;
        }
    }
}
