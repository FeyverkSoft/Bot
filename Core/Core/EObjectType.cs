using System;

namespace Core.Core
{
    /// <summary>
    /// Тип объектов которые надо получить
    /// </summary>
    [Flags]
    public enum EObjectType:Int64
    {
        /// <summary>
        /// Окно
        /// </summary>
        Window = 0x20000,
        /// <summary>
        /// Кнопка
        /// </summary>
        Button = 0x1,
        TrayButton = 0x2,
        TreeView = 0x4,
        ConsoleWindow = 0x8,
        Start = 0x10,
        Edit = 0x20,
        ComboBox = 0x40,
        FolderView = 0x80,
        Toolbar = 0x100,
        UICoreWindow = 0x200,
        Static = 0x400,
        TabControl = 0x800,
        StatusBar = 0x1000,
        ProgressBar = 0x2000,
        UIHWND = 0x4000,
        NetUIHWND = 0x8000,
        DirectUIHWND = 0x10000,
        /// <summary>
        /// Любой тип
        /// </summary>
        Any = 0x40000
    }
}
