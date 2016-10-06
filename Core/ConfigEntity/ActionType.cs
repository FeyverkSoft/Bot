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
        MouseMove = 0,
        /// <summary>
        /// Действие установки указателя в указанную позицию
        /// </summary>
        MouseSetPos = 2,
        /// <summary>
        /// Действие нажатия Правой кнопки мышки
        /// </summary>
        MouseRClick = 4,
        /// <summary>
        /// Действие нажатия Левой кнопки мышки
        /// </summary>
        MouseLClick = 8,
        /// <summary>
        /// Действие нажатия клавишы на клавиатуре если переданно несколько, то они нажимаются последовательно
        /// </summary>
        KeyBoard = 16,
        /// <summary>
        /// Действие нажатия одновременно нескольких клавиш на клавиатуре
        /// Если перереданно несколько, то они нажимаются одновременно :D
        /// </summary>
        KeyBoardKeys = 32,
        /// <summary>
        /// Действие сна бота на указанное время
        /// </summary>
        Sleep = 64,
        /// <summary>
        /// Цикл Особый оператор, не требующий фабрики
        /// </summary>
        Loop = 128,
        /// <summary>
        /// Описание действий который должен проверить и/или выполнить плагин
        /// </summary>
        PluginInvoke = 256,
        /// <summary>
        /// Выполнить действие для AI, возможно вытеснится плагинами
        /// </summary>
        Ai = 512,
        /// <summary>
        /// Особый оператор условного ветвления
        /// </summary>
        If = 1024,
        /// <summary>
        /// Оператор поиска окна с указанным названием, как только окно появится, выполение комманд продолжится
        /// </summary>
        ExpectWindow = 2048,
        /// <summary>
        /// Получить инфу об указанном объекте
        /// </summary>
        GetObject = 4096,
        /// <summary>
        /// Получить скриншот
        /// </summary>
        GetScreenshot = 8192,
        /// <summary>
        /// Фальшивый объект, для эмулации чего либо :) но не исполнения этого в реале
        /// </summary>
        Mock = 16384
    }
}
