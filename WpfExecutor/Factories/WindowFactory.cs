using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfExecutor.Model.About;
using WpfExecutor.Views.About;

namespace WpfExecutor.Factories
{
    internal static class WindowFactory
    {
        /// <summary>
        /// Создаёт окно о программе
        /// </summary>
        /// <returns></returns>
        public static AboutWindow CreateAboutWindow()
        {
            var viewModel = new AboutViewModel();
            var window = new AboutWindow(viewModel);
            return window;
        }
    }
}
