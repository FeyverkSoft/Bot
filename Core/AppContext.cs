using System;
using System.Collections.Generic;
using Core.Core;
using Core.Handlers;

namespace Core
{
    /// <summary>
    /// Контекст для инжектирования в приложении
    /// </summary>
    static class AppContext
    {
        private static readonly Dictionary<Type, Object> Dictionary = new Dictionary<Type, Object>();

        static AppContext()
        {
            Dictionary.Add(typeof(IActionFactory), new DefaultActionFactory());
            Dictionary.Add(typeof(IKeyBoard), new NativeKeyBoard());
            Dictionary.Add(typeof(IMouse), new NativeMouse());
            Dictionary.Add(typeof(IWindowsProc), new NativeWindowsProc());
            Dictionary.Add(typeof(IExecutiveCore), new DefaultExecutiveCore(Get<IActionFactory>()));
        }

        public static T Get<T>() where T : class
        {
            return (T)Dictionary[typeof(T)];
        }
    }
}
