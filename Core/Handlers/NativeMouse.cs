using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Core.Core;
using LogWrapper;

namespace Core.Handlers
{
    /// <summary>
    /// Класс для эмуляции мышки
    /// </summary>
    public class NativeMouse : IMouse
    {
        private static readonly Random Rand = new Random();
        [DllImport("User32.dll")]
        private static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        [Flags]
        private enum MouseFlags : int
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            Absolute = 0x8000
        };

        /// <summary>
        /// API для ролучения позиции курсора
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>

        [DllImport("user32.dll")]
        static extern Boolean GetCursorPos(ref System.Drawing.Point lpPoint);

        /// <summary>
        /// Сместить указатель на указанное смешение по осям кустарно эмулируя неточное поведение указателя человека
        /// Юзать алгоритмы впадлу.
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void MouseMove(Int32 dx, Int32 dy)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseMove)}(dx: {dx}; dy: {dy});");
            Int32 absDx = Math.Abs(dx), absDy = Math.Abs(dy);
            var len = 15;
            var count = Math.Max(absDx, absDy) / len; //колличество отрезков длинной не более в 15px для плавного движения

            var x = GetArray(dx, count, len);
            var y = GetArray(dy, count, len);

            for (var i = 0; i < x.Length; i++)
            {
                if (AppConfig.Instance.RandomMouse)
                    mouse_event(MouseFlags.Move, (Int32)(x[i] + Rand.Next(0, 2)), (Int32)(y[i] + Rand.Next(0, 2)), 0, UIntPtr.Zero);
                else
                    mouse_event(MouseFlags.Move, (Int32)(x[i]), (Int32)(y[i]), 0, UIntPtr.Zero);
                var xd = Rand.Next(0, 5);//уличная магия
                Thread.Sleep(8 + xd + (xd % 3 > 0 ? i : 0));//при преближении к кнопке эмулируется некоторое торможение движения указателя, так как это делает человек :D
            }
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseMove)};");
        }

        /// <summary>
        /// Магическая функция разрезает маршрут на отрезки заданной длинны на указанное коелличество частей, забивая пустоты нулями
        /// </summary>
        /// <param name="x"></param>
        /// <param name="count"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        private Int32[] GetArray(Int32 x, Int32 count, Int32 len)
        {
            Int32 absDx = Math.Abs(x);
            var list = new List<Int32>();
            if (count > absDx)
            {
                var localCount = absDx / len;
                for (var i = 0; i < localCount; i++)
                    list.Add(x / localCount);
                for (var i = 0; i < count - localCount; i++)
                    list.Add(0);
                if (list.Count > 0)
                    list[0] += (x - list.Sum(z => z));
                return list.OrderBy(u => Rand.Next()).ToArray();
            }
            else
            {
                for (var i = 0; i < count; i++)
                    list.Add(x / count);
                if (list.Count > 0)
                    list[0] += (x - list.Sum(z => z));
                return list.OrderBy(u => Rand.Next()).ToArray();
            }
        }
        /// <summary>
        /// Указать точное положение курсора мышки
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MouseSetPos(Int32 x, Int32 y)
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseSetPos)}(x: {x}; y: {y});");
            if (x < 0 || y < 0)
                throw new ArgumentException(nameof(MouseSetPos));
            Thread.Sleep(10);


            var resolution = Screen.PrimaryScreen.Bounds.Size;
            mouse_event(MouseFlags.Absolute | MouseFlags.Move, (Int32)((65535.0 / resolution.Width) * x), (Int32)((65535.0 / resolution.Height) * y), 0, UIntPtr.Zero);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseSetPos)};");
        }

        /// <summary>
        /// Получить текущю позицию указателя
        /// </summary>
        /// <returns></returns>
        public Point GetCurrentPos()
        {
            var lpPoint = new System.Drawing.Point();
            // заполняем defPnt информацией о координатах мышки
            GetCursorPos(ref lpPoint);
            if (lpPoint.IsEmpty)
                throw new Exception("Mouse Point IS Empty!!!!");
            return lpPoint;
        }

        /// <summary>
        /// Выполнить клик левой кнопкй мышки
        /// </summary>
        public void MouseLeftCl()
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseLeftCl)}");
            mouse_event(MouseFlags.LeftDown, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(10);
            mouse_event(MouseFlags.LeftUp, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(10);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseLeftCl)}");
        }

        /// <summary>
        /// Выполнить клик правой кнопкой мышки
        /// </summary>
        public void MouseRightCl()
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseRightCl)}");
            mouse_event(MouseFlags.RightDown, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(10);
            mouse_event(MouseFlags.RightUp, 0, 0, 0, UIntPtr.Zero);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseRightCl)}");
            Thread.Sleep(10);
        }

        /// <summary>
        /// Выполнить нажатие и удерживание левой кнопкй мышки
        /// </summary>
        public void MouseLeftPress()
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseLeftPress)}");
            mouse_event(MouseFlags.LeftDown, 0, 0, 0, UIntPtr.Zero);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseLeftPress)}");
        }

        /// <summary>
        /// Выполнить отжатие левой кнопкй мышки
        /// </summary>
        public void MouseLeftUp()
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseLeftUp)}");
            mouse_event(MouseFlags.LeftUp, 0, 0, 0, UIntPtr.Zero);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseLeftUp)}");
        }

        /// <summary>
        /// Выполнить отжатие правой кнопкй мышки
        /// </summary>
        public void MouseRightUp()
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseRightUp)}");
            mouse_event(MouseFlags.RightUp, 0, 0, 0, UIntPtr.Zero);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseRightUp)}");
        }
        /// <summary>
        /// Выполнить нажатие и удерживание правой кнопкй мышки
        /// </summary>
        public void MouseRightPress()
        {
            Log.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseRightPress)}");
            mouse_event(MouseFlags.RightDown, 0, 0, 0, UIntPtr.Zero);
            Log.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseRightPress)}");
        }
    }
}
