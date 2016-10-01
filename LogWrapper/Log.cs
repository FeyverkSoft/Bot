using System;
using System.Diagnostics;
using System.IO;
using System.Text;
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
                using (var fs = new FileStream($"logs/{DateTime.Now.ToShortDateString()}.{level}", FileMode.OpenOrCreate | FileMode.Append))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
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
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        public static void WriteLine(Object o, LogLevel level = LogLevel.Info)
        {
            try
            {
                Debug.WriteLine(o.ToJson());
                using (var fs = new FileStream($"logs/{DateTime.Now.ToShortDateString()}.{level}", FileMode.OpenOrCreate | FileMode.Append))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
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
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        public static void Write(String s, LogLevel level = LogLevel.Info)
        {
            try
            {
                Debug.WriteLine(s);
                using (var fs = new FileStream($"logs/{DateTime.Now.ToShortDateString()}.{level}", FileMode.OpenOrCreate | FileMode.Append))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
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
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        public static void Write(Object o, LogLevel level = LogLevel.Info)
        {
            try
            {
                Debug.Write(o.ToJson());
                using (var fs = new FileStream($"logs/{DateTime.Now.ToShortDateString()}.{level}", FileMode.OpenOrCreate | FileMode.Append))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
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
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
    }
}
