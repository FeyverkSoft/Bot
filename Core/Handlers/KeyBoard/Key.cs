using System;
using Core.Core;

namespace Core.Handlers.KeyBoard
{
    internal class Key
    {
        public KeyName KeyName { get; set; }
        public Int16 VKey { get; set; }
        public Int16 DxInputKey { get; set; }

        public Key(KeyName name, Int16 vKey, Int16 dxKey)
        {
            KeyName = name;
            VKey = vKey;
            DxInputKey = dxKey;
        }
        public Key() { }
    }
}
