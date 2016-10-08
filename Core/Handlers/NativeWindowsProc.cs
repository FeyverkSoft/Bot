using System;
using System.Runtime.InteropServices;
using System.Text;
using Core.Core;

namespace Core.Handlers
{
    /// <summary>
    /// Класс для работы с окнами
    /// </summary>
    public class NativeWindowsProc : IWindowsProc
    {
        delegate Boolean EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        /// <summary>
        /// Вызвать перечисление всех окон
        /// </summary>
        /// <param name="lpEnumFunc"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        /// <summary>
        /// По идентификароту дискриптора получает, является ли окно видимым или нет
        /// </summary>
        /// <param name="hWnd">Дискриптор окна</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern Boolean IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// По дискриптору возвращает название окна
        /// </summary>
        /// <param name="hWnd">Дискриптор окна</param>
        /// <param name="lpString"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern Int32 GetWindowText(IntPtr hWnd, StringBuilder lpString, Int32 nMaxCount);
        /// <summary>
        /// Получить информацию об окне со следующим заголовком
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        static extern Int32 GetWindowTextLength(IntPtr hWnd);
        string GetWindowText(IntPtr hWnd)
        {
            int len = GetWindowTextLength(hWnd) + 1;
            StringBuilder sb = new StringBuilder(len);
            len = GetWindowText(hWnd, sb, len);
            return sb.ToString(0, len);
        }
        /// <summary>
        /// Вернуть размеры и позицию окна
        /// </summary>
        /// <param name="hwnd">Дескриптор окна, для которого ищем размеры и позицию</param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern Boolean GetWindowRect(IntPtr hwnd, ref RECT rectangle);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public Int32 X;
            public Int32 Y;
            public Int32 Width;
            public Int32 Height;
        }
        /// <summary>
        /// Показать окно по номеру дискриптора
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern Int32 SetForegroundWindow([In] IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, Point point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 GetClassName(IntPtr hWnd, StringBuilder lpClassName, Int32 nMaxCount);
        private static String GetWindowClassName(IntPtr hWnd)
        {
            StringBuilder buffer = new StringBuilder(128);
            GetClassName(hWnd, buffer, buffer.Capacity);
            return buffer.ToString();
        }


        /// <summary>
        /// Получть информацию об окне с указанным заголовком
        /// </summary>
        /// <param name="title"></param>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public WinInfo GetWinInfo(String title, ESearchParam searchParam = ESearchParam.Contained)
        {
            var rets = new WinInfo(title, 0, 0,0,0, (IntPtr)0, false);
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    var desTitle = GetWindowText(hWnd);
                    switch (searchParam)
                    {
                        case ESearchParam.End:
                            if (title != null && desTitle.Trim().ToLower().EndsWith(title?.Trim().ToLower()))
                            {
                                RECT r = new RECT();
                                GetWindowRect(hWnd, ref r);
                                rets = new WinInfo(desTitle, r.X, r.Y,r.Width,r.Height, hWnd, true);
                            }
                            break;
                        case ESearchParam.Start:
                            if (title != null && desTitle.Trim().ToLower().StartsWith(title?.Trim().ToLower()))
                            {
                                RECT r = new RECT();
                                GetWindowRect(hWnd, ref r);
                                rets = new WinInfo(desTitle, r.X, r.Y, r.Width, r.Height, hWnd, true);
                            }
                            break;
                        case ESearchParam.Contained:
                        default:
                            if (title != null && desTitle.Trim().ToLower().Contains(title?.Trim().ToLower()))
                            {
                                RECT r = new RECT();
                                GetWindowRect(hWnd, ref r);
                                rets = new WinInfo(desTitle, r.X, r.Y, r.Width, r.Height, hWnd, true);
                            }
                            break;
                    }

                }
                return true;
            }, IntPtr.Zero);
            return rets;
        }

        /// <summary>
        /// Передать фокус окну с указанным индикатором дескриптора
        /// </summary>
        /// <param name="winId">дескриптор</param>
        public void ShowWindow(IntPtr winId)
        {
            ShowWindow(winId, 1);
            SetForegroundWindow(winId);
        }

        /// <summary>
        /// Возвращает объект, который содержит указанную точку
        /// </summary>
        /// <param name="p">точка, которая должна содержаться в объекте</param>
        /// <returns></returns>
        public ObjectInfo GetObjectFromPoint(Point p)
        {
            throw new NotImplementedException();
        }
    }
}
