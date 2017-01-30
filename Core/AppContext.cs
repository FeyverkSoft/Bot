using System;
using System.Collections.Generic;
using Core.Core;
using Core.Handlers;
using Core.Handlers.Factory;

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
            SingDictionary.Add(typeof(IActionExecutorFactory), new DefaultActionExecutorFactory());
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
                    return CoreWrapperProxy.CreateInstance(new DefaultExecutiveCore(new DefaultActionExecutorFactory(), Get<IConfigValidator>())) as T;
                case nameof(IActionExecutorFactory):
                    return new DefaultActionExecutorFactory() as T;
            }
            throw new NotSupportedException(nameof(T));
        }
    }
}
