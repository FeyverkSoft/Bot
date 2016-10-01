using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
namespace Core.Handlers
{
    /// <summary>
    /// Класс для эмуляции мышки
    /// </summary>
    public class Mouse : IMouse
    {
        private static Random rand = new Random();
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
        /// Сместить указатель на указанное смешение по осям кустарно эмулируя неточное поведение указателя человека
        /// Юзать алгоритмы впадлу.
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void MouseMove(Int32 dx, Int32 dy)
        {
            Debug.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseMove)}(dx: {dx}; dy: {dy});");
            if (Math.Abs(dx) >= 10 && Math.Abs(dy) >= 10 && Math.Abs(dx) < 40 && Math.Abs(dy) < 40)
            {
                float x = dx / 10, y = dy / 10;
                for (var i = 0; i < 10; i++)
                {
                    mouse_event(MouseFlags.Move, (Int32)(x + rand.Next(0, 2)), (Int32)(y + rand.Next(0, 2)), 0, UIntPtr.Zero);
                    Thread.Sleep(10);
                }
            }
            else if (Math.Abs(dx) >= 40 && Math.Abs(dy) >= 40 && Math.Abs(dx) < 400 && Math.Abs(dy) < 400)
            {
                float x = dx / 20, y = dy / 20;
                for (var i = 0; i < 20; i++)
                {
                    mouse_event(MouseFlags.Move, (Int32)(x + rand.Next(0, 3)), (Int32)(y + rand.Next(0, 3)), 0, UIntPtr.Zero);
                    Thread.Sleep(5);
                }
            }
            else if (Math.Abs(dx) > 400 && Math.Abs(dy) > 400)
            {
                float x = dx / 50, y = dy / 50;
                for (var i = 0; i < 50; i++)
                {
                    mouse_event(MouseFlags.Move, (Int32)(x + rand.Next(0, 3)), (Int32)(y + rand.Next(0, 3)), 0, UIntPtr.Zero);
                    Thread.Sleep(10);
                }
            }
            else
            {
                mouse_event(MouseFlags.Move, dx, dy, 0, UIntPtr.Zero);
            }
            Debug.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseMove)};");
        }

        /// <summary>
        /// Указать точное положение курсора мышки
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MouseSetPos(Int32 x, Int32 y)
        {
            Debug.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseSetPos)}(x: {x}; y: {y});");
            if (x < 0 || y < 0)
                throw new ArgumentException(nameof(MouseSetPos));
            Thread.Sleep(10);


            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            mouse_event(MouseFlags.Absolute | MouseFlags.Move, (Int32)((65535.0 / resolution.Width) * x), (Int32)((65535.0 / resolution.Height) * y), 0, UIntPtr.Zero);
            Debug.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseSetPos)};");
        }

        /// <summary>
        /// Выполнить клик левой кнопкй мышки
        /// </summary>
        public void MouseLeftCl()
        {
            Debug.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseLeftCl)}");
            mouse_event(MouseFlags.LeftDown, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(10);
            mouse_event(MouseFlags.LeftUp, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(10);
            Debug.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseLeftCl)}");
        }

        /// <summary>
        /// Выполнить клик правой кнопкой мышки
        /// </summary>
        public void MouseRightCl()
        {
            Debug.WriteLine($"-- BEGIN -- {GetType().Name}.{nameof(MouseRightCl)}");
            mouse_event(MouseFlags.RightDown, 0, 0, 0, UIntPtr.Zero);
            Thread.Sleep(10);
            mouse_event(MouseFlags.RightUp, 0, 0, 0, UIntPtr.Zero);
            Debug.WriteLine($"-- END -- {GetType().Name}.{nameof(MouseRightCl)}");
            Thread.Sleep(10);
        }
    }
}
