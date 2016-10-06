using System;

namespace Core.Core
{
    /// <summary>
    /// Класс содержащий информацию об окне
    /// </summary>
    public class WinInfo : BaseObjectInfo
    {

        /// <summary>
        /// Было ли окно найдено?
        /// </summary>
        public Boolean IsFound { get; private set; }

        public WinInfo(String title, Int32 posX, Int32 posY, IntPtr descriptor, Boolean isFound = true) :
            base(descriptor, posX, posY, title)
        {
            IsFound = isFound;
        }

        public WinInfo(String title, Point point, IntPtr descriptor, Boolean isFound = true) : base(descriptor, point, title)
        {
            IsFound = isFound;
        }
    }
}
