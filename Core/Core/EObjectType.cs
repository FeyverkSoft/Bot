using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Core
{
    /// <summary>
    /// Тип объектов которые надо получить
    /// </summary>
    [Flags]
    public enum EObjectType
    {
        /// <summary>
        /// Окно
        /// </summary>
        Window,
        /// <summary>
        /// Кнопка
        /// </summary>
        Button,
        TrayButton,
        TreeView,
        ConsoleWindow,
        Start,
        Edit,
        ComboBox,
        FolderView,
        Toolbar,
        UICoreWindow,
        Static,
        TabControl,
        StatusBar,
        ProgressBar,
        UIHWND,
        NetUIHWND,
        DirectUIHWND,

        /// <summary>
        /// Любой тип
        /// </summary>
        Any
    }
}
