using System;
using Core.Core;

namespace Core.Handlers
{
    /// <summary>
    /// Класс для работы с окнами
    /// </summary>
    public interface IWindowsProc
    {
        /// <summary>
        /// Получть информацию об окне с указанным заголовком
        /// </summary>
        /// <param name="title"></param>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        WinInfo GetWinInfo(String title, ESearchParam searchParam = ESearchParam.Contained);

        /// <summary>
        /// Передать фокус окну с указанным индикатором дескриптора
        /// </summary>
        /// <param name="winId">дескриптор</param>
        void ShowWindow(IntPtr winId);

        /// <summary>
        /// Возвращает объект, который содержит указанную точку
        /// </summary>
        /// <param name="p">точка, которая должна содержаться в объекте</param>
        /// <returns></returns>
        ObjectInfo GetObjectFromPoint(Point p);
    }
}