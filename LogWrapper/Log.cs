using System;

namespace LogWrapper
{
    public static class Log
    {
        private static Logger Logger { get; set; }

        public static void Init(String logFilePath = null)
        {
            Logger = !String.IsNullOrEmpty(logFilePath) ? new Logger(logFilePath) : new Logger("Logs/");
        }

        /// <summary>
        /// Записать лог
        /// </summary>
        /// <param name="o">Сообщение</param>
        /// <param name="level">Уровень</param>
        public static void WriteLine(Object o, LogLevel level = LogLevel.Info)
        {
            if (Logger == null)
                Init();
            Logger?.WriteLine(o, level);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">Сообщение</param>
        /// <param name="level">Уровень</param>
        public static void Write(Object o, LogLevel level = LogLevel.Info)
        {
            if (Logger == null)
                Init();
            Logger?.Write(o, level);
        }
    }
}
