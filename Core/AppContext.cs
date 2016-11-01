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
        /// <summary>
        /// Список сингелтонов
        /// </summary>
        private static readonly Dictionary<Type, Object> SingDictionary = new Dictionary<Type, Object>();

        static AppContext()
        {
            SingDictionary.Add(typeof(IActionFactory), new DefaultActionFactory());
            SingDictionary.Add(typeof(IKeyBoard), new NativeKeyBoard());
            SingDictionary.Add(typeof(IMouse), new NativeMouse());
            SingDictionary.Add(typeof(IWindowsProc), new NativeWindowsProc());
            SingDictionary.Add(typeof(IConfigValidator), new ConfigValidator());
        }

        public static T Get<T>() where T : class
        {
            if (SingDictionary.ContainsKey(typeof(T)))
                return (T)SingDictionary[typeof(T)];
            switch (typeof(T).Name)
            {
                case nameof(IExecutiveCore):
                    return new DefaultExecutiveCore(new DefaultActionFactory(), Get<IConfigValidator>()) as T;
                case nameof(IActionFactory):
                    return new DefaultActionFactory() as T;
            }
            throw new NotSupportedException(nameof(T));
        }
    }
}
