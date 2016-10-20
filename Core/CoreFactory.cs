using Core.Core;

namespace Core
{
    /// <summary>
    /// Фабрика ядра
    /// </summary>
    public static class CoreFactory
    {
        /// <summary>
        /// Получить экземпляр ядра по умолчанию
        /// </summary>
        /// <returns></returns>
        public static IExecutiveCore GetCore()
        {
            if (AppConfig.DefaultCore)
                AppContext.Get<IExecutiveCore>();
            return new DefaultExecutiveCore(new DefaultActionFactory());
        }
    }
}
