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
        /// <returns></returns>
        WinInfo GetWinInfo(String title);

        /// <summary>
        /// Передать фокус окну с указанным индикатором дескриптора
        /// </summary>
        /// <param name="winId">дескриптор</param>
        void ShowWindow(IntPtr winId);
    }
}