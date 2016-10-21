using System;
using Core.Attributes;

namespace Core.ConfigEntity
{
    /// <summary>
    /// Типы доступных боту действий
    /// </summary>
    [LocDescription("ActionType")]
    public enum ActionType : Int32
    {
        /// <summary>
        /// Действие перемещения мышки в нужную позицию
        /// </summary>
        [LocDescription("ActionType_MouseMove")]
        MouseMove,
        /// <summary>
        /// Действие установки указателя в указанную позицию
        /// </summary>
        [LocDescription("ActionType_MouseSetPos")]
        MouseSetPos,
        /// <summary>
        /// Действие клика Правой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseRClick")]
        MouseRClick,
        /// <summary>
        /// Действие нажатия Правой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseRPress")]
        MouseRPress,
        /// <summary>
        /// Действие отпускания Правой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseRUp")]
        MouseRUp,
        /// <summary>
        /// Действие нажатия Левой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseLPress")]
        MouseLPress,
        /// <summary>
        /// Действие отпускания Левой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseLUp")]
        MouseLUp,
        /// <summary>
        /// Действие клика Левой кнопки мышки
        /// </summary>
        [LocDescription("ActionType_MouseLClick")]
        MouseLClick,
        /// <summary>
        /// Получить позицию курсора
        /// </summary>
        [LocDescription("ActionType_GetMousePos")]
        GetMousePos,
        /// <summary>
        /// Действие нажатия клавишы на клавиатуре если переданно несколько, то они нажимаются последовательно
        /// </summary>
        [LocDescription("ActionType_KeyBoard")]
        KeyBoard,
        /// <summary>
        /// Действие нажатия одновременно нескольких клавиш на клавиатуре
        /// Если перереданно несколько, то они нажимаются одновременно :D
        /// </summary>
        [LocDescription("ActionType_KeyBoardKeys")]
        KeyBoardKeys,
        /// <summary>
        /// Действие сна бота на указанное время
        /// </summary>
        [LocDescription("ActionType_Sleep")]
        Sleep,
        /// <summary>
        /// Цикл Особый оператор, не требующий фабрики
        /// </summary>
        [LocDescription("ActionType_Loop")]
        Loop,
        /// <summary>
        /// Описание действий который должен проверить и/или выполнить плагин
        /// </summary>
        [LocDescription("ActionType_PluginInvoke")]
        PluginInvoke,
        /// <summary>
        /// Особый оператор условного ветвления
        /// </summary>
        [LocDescription("ActionType_If")]
        If,
        /// <summary>
        /// Оператор поиска окна с указанным названием, как только окно появится, выполение комманд продолжится
        /// </summary>
        [LocDescription("ActionType_ExpectWindow")]
        ExpectWindow,
        /// <summary>
        /// Получить инфу об указанном объекте
        /// </summary>
        [LocDescription("ActionType_GetObject")]
        GetObject,
        /// <summary>
        /// Получить скриншот
        /// </summary>
        [LocDescription("ActionType_GetScreenshot")]
        GetScreenshot,
        /// <summary>
        /// Фальшивый объект, для эмулации чего либо :) но не исполнения этого в реале
        /// </summary>
        [LocDescription("ActionType_Mock")]
        Mock,
        /// <summary>
        /// Действие которое знает как выполнять только бот
        /// </summary>
        [LocDescription("ActionType_PluginAct")]
        PluginAct,
        /// <summary>
        /// Отправить сообщение на указанный канал связи
        /// </summary>
        [LocDescription("ActionType_SendMessage")]
        SendMessage
    }
}
