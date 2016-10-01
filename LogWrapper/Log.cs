using System;
using System.Diagnostics;
using LogWrapper.Helpers;

namespace LogWrapper
{
    public static class Log
    {
        public static void WriteLine(String s)
        {
            Debug.WriteLine(s);
        }
        public static void WriteLine(Object o)
        {
            Debug.WriteLine(o.ToJson());
        }

        public static void Write(String s)
        {
            Debug.Write(s);
        }
        public static void Write(Object o)
        {
            Debug.Write(o.ToJson());
        }
    }
}
