using System;
using Core.Core;

namespace Core.Handlers.KeyBoard
{
    public class Key
    {
        public KeyName KeyName { get; set; }
        public UInt16 VKey { get; set; }
        public UInt16 DxInputKey { get; set; }

        public Key(KeyName name, UInt16 vKey, UInt16 dxKey)
        {
            KeyName = name;
            VKey = vKey;
            DxInputKey = dxKey;
        }
        public Key() { }
    }
}
