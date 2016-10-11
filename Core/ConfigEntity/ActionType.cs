﻿using System;

namespace Core.ConfigEntity
{
    /// <summary>
    /// Типы доступных боту действий
    /// </summary>
    public enum ActionType : Int32
    {
        /// <summary>
        /// Действие перемещения мышки в нужную позицию
        /// </summary>
        MouseMove,
        /// <summary>
        /// Действие установки указателя в указанную позицию
        /// </summary>
        MouseSetPos,
        /// <summary>
        /// Действие клика Правой кнопки мышки
        /// </summary>
        MouseRClick,
        /// <summary>
        /// Действие нажатия Правой кнопки мышки
        /// </summary>
        MouseRPress,
        /// <summary>
        /// Действие отпускания Правой кнопки мышки
        /// </summary>
        MouseRUp,
        /// <summary>
        /// Действие нажатия Левой кнопки мышки
        /// </summary>
        MouseLPress,
        /// <summary>
        /// Действие отпускания Левой кнопки мышки
        /// </summary>
        MouseLUp,
        /// <summary>
        /// Действие клика Левой кнопки мышки
        /// </summary>
        MouseLClick,
        /// <summary>
        /// Получить позицию курсора
        /// </summary>
        GetMousePos,
        /// <summary>
        /// Действие нажатия клавишы на клавиатуре если переданно несколько, то они нажимаются последовательно
        /// </summary>
        KeyBoard,
        /// <summary>
        /// Действие нажатия одновременно нескольких клавиш на клавиатуре
        /// Если перереданно несколько, то они нажимаются одновременно :D
        /// </summary>
        KeyBoardKeys,
        /// <summary>
        /// Действие сна бота на указанное время
        /// </summary>
        Sleep,
        /// <summary>
        /// Цикл Особый оператор, не требующий фабрики
        /// </summary>
        Loop,
        /// <summary>
        /// Описание действий который должен проверить и/или выполнить плагин
        /// </summary>
        PluginInvoke,
        /// <summary>
        /// Особый оператор условного ветвления
        /// </summary>
        If,
        /// <summary>
        /// Оператор поиска окна с указанным названием, как только окно появится, выполение комманд продолжится
        /// </summary>
        ExpectWindow,
        /// <summary>
        /// Получить инфу об указанном объекте
        /// </summary>
        GetObject,
        /// <summary>
        /// Получить скриншот
        /// </summary>
        GetScreenshot,
        /// <summary>
        /// Фальшивый объект, для эмулации чего либо :) но не исполнения этого в реале
        /// </summary>
        Mock,
        /// <summary>
        /// Действие которое знает как выполнять только бот
        /// </summary>
        PluginAct
    }
}
