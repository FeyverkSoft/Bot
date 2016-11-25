using System;
using CommonLib.Attributes;

namespace Core.ConfigEntity
{
    /// <summary>
    /// Типы доступных боту действий
    /// </summary>
    [LocDescription("ActionType", typeof(Resources.CoreText))]
    public enum ActionType : Int32
    {
        /// <summary>
        /// Действие перемещения мышки в нужную позицию
        /// </summary>
        [LocDescription("ActionType_MouseMove", typeof(Resources.CoreText))]
        MouseMove,
        /// <summary>
        /// Действие установки указателя в указанную позицию
        /// </summary>
        [LocDescription("ActionType_MouseSetPos", typeof(Resources.CoreText))]
        MouseSetPos,
        /// <summary>
        /// Действие клика Правой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseRClick", typeof(Resources.CoreText))]
        MouseRClick,
        /// <summary>
        /// Действие нажатия Правой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseRPress", typeof(Resources.CoreText))]
        MouseRPress,
        /// <summary>
        /// Действие отпускания Правой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseRUp", typeof(Resources.CoreText))]
        MouseRUp,
        /// <summary>
        /// Действие нажатия Левой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseLPress", typeof(Resources.CoreText))]
        MouseLPress,
        /// <summary>
        /// Действие отпускания Левой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseLUp", typeof(Resources.CoreText))]
        MouseLUp,
        /// <summary>
        /// Действие клика Левой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseLClick", typeof(Resources.CoreText))]
        MouseLClick,
        /// <summary>
        /// Получить позицию курсора
        /// </summary>
        [LocDescription("ActionType_GetMousePos", typeof(Resources.CoreText))]
        GetMousePos,
        /// <summary>
        /// Действие нажатия клавишы на клавиатуре если переданно несколько, то они нажимаются последовательно
        /// </summary>
        [LocDescription("ActionType_KeyBoard", typeof(Resources.CoreText))]
        KeyBoardPressKey,
        /// <summary>
        /// Действие нажатия одновременно нескольких клавиш на клавиатуре
        /// Если перереданно несколько, то они нажимаются одновременно :D
        /// </summary>
        [LocDescription("ActionType_KeyBoardKeys", typeof(Resources.CoreText))]
        KeyBoardShortcut,
        [LocDescription("ActionType_KeyBoardAction", typeof(Resources.CoreText))]
        KeyBoardAction,
        /// <summary>
        /// Действие сна бота на указанное время
        /// </summary>
        [LocDescription("ActionType_Sleep", typeof(Resources.CoreText))]
        Sleep,
        /// <summary>
        /// Цикл Особый оператор, не требующий фабрики
        /// </summary>
        [LocDescription("ActionType_Loop", typeof(Resources.CoreText))]
        Loop,
        /// <summary>
        /// Описание действий который должен проверить и/или выполнить плагин
        /// </summary>
        [LocDescription("ActionType_PluginInvoke", typeof(Resources.CoreText))]
        PluginInvoke,
        /// <summary>
        /// Особый оператор условного ветвления
        /// </summary>
        [LocDescription("ActionType_If", typeof(Resources.CoreText))]
        If,
        /// <summary>
        /// Оператор поиска окна с указанным названием, как только окно появится, выполение комманд продолжится
        /// </summary>
        [LocDescription("ActionType_ExpectWindow", typeof(Resources.CoreText))]
        ExpectWindow,
        /// <summary>
        /// Получить инфу об указанном объекте
        /// </summary>
        [LocDescription("ActionType_GetObject", typeof(Resources.CoreText))]
        GetObject,
        /// <summary>
        /// Получить скриншот
        /// </summary>
        [LocDescription("ActionType_GetScreenshot", typeof(Resources.CoreText))]
        GetScreenshot,
        /// <summary>
        /// Фальшивый объект, для эмулации чего либо :) но не исполнения этого в реале
        /// </summary>
        [LocDescription("ActionType_Mock", typeof(Resources.CoreText))]
        Mock,
        /// <summary>
        /// Действие которое знает как выполнять только бот
        /// </summary>
        [LocDescription("ActionType_PluginAct", typeof(Resources.CoreText))]
        PluginAct,
        /// <summary>
        /// Отправить сообщение на указанный канал связи
        /// </summary>
        [LocDescription("ActionType_SendMessage", typeof(Resources.CoreText))]
        SendMessage,
        /// <summary>
        /// Метка для GOTO
        /// </summary>
        [LocDescription("ActionType_Label", typeof(Resources.CoreText))]
        Label,
        /// <summary>
        /// Метка для GOTO
        /// </summary>
        [LocDescription("ActionType_GOTO", typeof(Resources.CoreText))]
        GOTO,
        /// <summary>
        /// Метка для GOTO
        /// </summary>
        [LocDescription("ActionType_Stack", typeof(Resources.CoreText))]
        Stack,
    }
}
