using System;

namespace Core.Events
{
    public delegate void PrintMessageEvent(String message);

    public delegate void AbortEvent();
}
