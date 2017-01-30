using System;
using System.IO;
using LogWrapper;

namespace Core.Helpers
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// Создать директорию
        /// </summary>
        /// <param name="logPath"></param>
        public static void CreateDirectory(String logPath)
        {
            if (!String.IsNullOrEmpty(logPath))
            {
                if (!String.IsNullOrEmpty(logPath) && !Directory.Exists(logPath))
                {
                    try
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    catch (Exception e)
                    {
                        Log.WriteLine(e, LogLevel.Error);
                    }
                }
            }
        }
    }
}