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

        public WinInfo(String title, Int32 posX, Int32 posY, Int32 widthX, Int32 heightY, IntPtr descriptor, Boolean isFound = true) :
            base(descriptor, posX, posY, widthX, heightY, title)
        {
            IsFound = isFound;
        }

        public WinInfo(String title, Point point, Size size, IntPtr descriptor, Boolean isFound = true) : base(descriptor, point, size, title)
        {
            IsFound = isFound;
        }
    }
}
