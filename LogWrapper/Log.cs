using System;
using System.Diagnostics;
using System.IO;
using LogWrapper.Helpers;

namespace LogWrapper
{
    public static class Log
    {
        public static void WriteLine(String s, LogLevel level = LogLevel.Info)
        {
            try
            {
                Debug.WriteLine(s);
                using (StreamWriter sw = new StreamWriter($"\\logs\\{DateTime.Now.ToShortDateString()}", true))
                {
                    sw.WriteLine(new
                    {
                        LogDateTime = DateTime.Now,
                        LogLevel = level,
                        Log = s
                    }.ToJson());
                    sw.Flush();
                }
            }
            catch { }
        }
        public static void WriteLine(Object o, LogLevel level = LogLevel.Info)
        {
            try
            {
                Debug.WriteLine(o.ToJson());
                using (StreamWriter sw = new StreamWriter($"\\logs\\{DateTime.Now.ToShortDateString()}", true))
                {
                    sw.WriteLine(new
                    {
                        LogDateTime = DateTime.Now,
                        LogLevel = level,
                        Log = o
                    }.ToJson());
                    sw.Flush();
                }
            }
            catch { }
        }

        public static void Write(String s, LogLevel level = LogLevel.Info)
        {
            try
            {
                Debug.WriteLine(s);
                using (StreamWriter sw = new StreamWriter($"\\logs\\{DateTime.Now.ToShortDateString()}", true))
                {
                    sw.Write(new
                    {
                        LogDateTime = DateTime.Now,
                        LogLevel = level,
                        Log = s
                    }.ToJson());
                    sw.Flush();
                }
            }
            catch { }
        }
        public static void Write(Object o, LogLevel level = LogLevel.Info)
        {
            try
            {
                Debug.Write(o.ToJson());
                using (StreamWriter sw = new StreamWriter($"\\logs\\{DateTime.Now.ToShortDateString()}", true))
                {
                    sw.Write(new
                    {
                        LogDateTime = DateTime.Now,
                        LogLevel = level,
                        Log = o
                    }.ToJson());
                    sw.Flush();
                }
            }
            catch { }
        }
    }
}
