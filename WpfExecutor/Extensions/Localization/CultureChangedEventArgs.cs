using System;
using System.Globalization;

namespace WpfExecutor.Extensions.Localization
{
    public class CultureChangedEventArgs : EventArgs
    {
        public CultureChangedEventArgs(CultureInfo culture)
        {
            Culture = culture;
        }

        public CultureInfo Culture { get; }
    }
}
