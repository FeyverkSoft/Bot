using System;
using ImgComparer.Factories;

namespace ImgComparer
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            WindowFactory.CreateMainWindow(null).ShowDialog();
            Console.Read();
        }
    }
}
