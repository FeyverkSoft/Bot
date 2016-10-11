using System;
using Core.Core;

namespace Core.Handlers
{
    /// <summary>
    /// Интерфейс Класса для эмуляции мышки
    /// </summary>
    public interface IMouse
    {
        /// <summary>
        /// Выполнить клик левой кнопкй мышки
        /// </summary>
        void MouseLeftCl();
        /// <summary>
        /// Выполнить клик правой кнопкой мышки
        /// </summary>
        void MouseRightCl();
        /// <summary>
        /// Выполнить нажатие и удерживание левой кнопкй мышки
        /// </summary>
        void MouseLeftPress();
        /// <summary>
        /// Выполнить отжатие левой кнопкй мышки
        /// </summary>
        void MouseLeftUp();
        /// <summary>
        /// Выполнить нажатие и удерживание правой кнопкй мышки
        /// </summary>
        void MouseRightPress();
        /// <summary>
        /// Выполнить отжатие правой кнопкй мышки
        /// </summary>
        void MouseRightUp();
        /// <summary>
        /// Сместить указатель на указанное смешение по осям кустарно эмулируя неточное поведение указателя человека
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        void MouseMove(Int32 dx, Int32 dy);
        /// <summary>
        /// Указать точное положение курсора мышки
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void MouseSetPos(Int32 x, Int32 y);

        /// <summary>
        /// Получить текущю позицию указателя
        /// </summary>
        /// <returns></returns>
        Point GetCurrentPos();

    }
}