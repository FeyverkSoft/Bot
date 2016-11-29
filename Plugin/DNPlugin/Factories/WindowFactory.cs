using System;
using Core.ConfigEntity;
using Core.ConfigEntity.ActionObjects;
using ImgComparer.Model;

namespace ImgComparer.Factories
{
    internal static class WindowFactory
    {
        /// <summary>
        /// Создаёт главное окно
        /// </summary>
        /// <returns></returns>
        public static MainWindow CreateMainWindow(String[] args)
        {
            var window = new MainWindow(new MainWindowModel());
            return window;
        }
    }
}
