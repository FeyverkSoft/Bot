using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LogWrapper.Helpers;

namespace LogWrapper
{
    public class Logger
    {
        /// <summary>
        /// Писать ассинхроно?
        /// </summary>
        Boolean Async { get; set; } = true;

        /// <summary>
        /// Писать в файл?
        /// </summary>
        Boolean FileWrite { get; set; } = true;

        /// <summary>
        /// Директория логов
        /// </summary>
        String LogsPath { get; set; } = "Logs/";

        private readonly Object _lock = new Object();

        public Logger()
        {
        }

        public Logger(String logPath, Boolean async = true)
        {
            FileWrite = true;
            LogsPath = logPath;
            Async = async;
            CreateDirectory(logPath);
        }

        /// <summary>
        /// Создать директорию
        /// </summary>
        /// <param name="logPath"></param>
        private void CreateDirectory(String logPath)
        {
            if (!String.IsNullOrEmpty(logPath))
            {
                if (!string.IsNullOrEmpty(logPath) && !Directory.Exists(logPath))
                {
                    try
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    catch (Exception e)
                    {
                        Debug.Fail(e.ToJson());
                    }
                }
            }
            else
            // кинуть исключение нельзя, так как мы можем потом его опять перехватить и записать в лог, и опять словим, кароче дичь
                Debug.Fail("logPath cannot be null!");
        }

        /// <summary>
        /// Записать лог в файл
        /// </summary>
        /// <param name="o"></param>
        /// <param name="level"></param>
        /// <param name="writeLine"></param>
        private void WriteLogInternal(Object o, LogLevel level = LogLevel.Info, Boolean writeLine = true)
        {
            lock (_lock)
            {
                if (writeLine)
                {
                    if (o != null) Debug.WriteLine(o.ToJson());
                }
                else if (o != null) Debug.Write(o.ToString());

                if (FileWrite && !string.IsNullOrEmpty(LogsPath))
                {
                    try
                    {
                        if (writeLine)
                            File.AppendAllText($"{LogsPath}{DateTime.Now.ToShortDateString()}.{level}", new
                            {
                                LogDateTime = DateTime.Now,
                                LogLevel = level,
                                Log = o
                            }.ToJson() + Environment.NewLine,
                                Encoding.UTF8);
                        else if (o != null)
                            File.AppendAllText($"{LogsPath}{DateTime.Now.ToShortDateString()}.{level}", o.ToString(),
                                Encoding.UTF8);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                }
            }
        }

        /// <summary>
        /// Записать новую строчку лога в файл
        /// </summary>
        /// <param name="o"></param>
        /// <param name="level"></param>
        public void WriteLine(Object o, LogLevel level = LogLevel.Info)
        {
            if (Async)
            {
                Task.Run(() => WriteLogInternal(o, level));
            }
            else
            {
                WriteLogInternal(o, level);
            }
        }
        /// <summary>
        /// Записать лог в файл
        /// </summary>
        /// <param name="o"></param>
        /// <param name="level"></param>
        public void Write(Object o, LogLevel level = LogLevel.Info)
        {
            if (Async)
            {
                Task.Run(() => WriteLogInternal(o, level, false));
            }
            else
            {
                WriteLogInternal(o, level, false);
            }
        }
    }
}