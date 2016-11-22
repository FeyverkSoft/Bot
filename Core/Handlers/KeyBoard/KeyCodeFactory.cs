using System.Collections.Generic;
using Core.Core;

namespace Core.Handlers.KeyBoard
{
    public static class KeyCodeFactory
    {
        private static readonly Dictionary<KeyName, Key> KeyArray;

        static KeyCodeFactory()
        {
            KeyArray = new Dictionary<KeyName, Key>
            {
                {KeyName.Escape, new Key(KeyName.Escape, 0x1b, 0x01) },
                {KeyName.D1, new Key(KeyName.D1, 0x31, 0x02) },
                {KeyName.D2, new Key(KeyName.D2, 0x32, 0x03) },
                {KeyName.D3, new Key(KeyName.D3, 0x33, 0x04) },
                {KeyName.D4, new Key(KeyName.D4, 0x34, 0x05) },
                {KeyName.D5, new Key(KeyName.D5, 0x35, 0x06) },
                {KeyName.D6, new Key(KeyName.D6, 0x36, 0x07) },
                {KeyName.D7, new Key(KeyName.D7, 0x37, 0x08) },
                {KeyName.D8, new Key(KeyName.D8, 0x38, 0x09) },
                {KeyName.D9, new Key(KeyName.D9, 0x39, 0x0A) },
                {KeyName.D0, new Key(KeyName.D0, 0x30, 0x0B) },
                {KeyName.Minus, new Key(KeyName.OemMinus, 0xbd, 0x0C) },
                {KeyName.EQUALS, new Key(KeyName.EQUALS, 0xBB, 0x0D) },
                {KeyName.BackSpace, new Key(KeyName.BackSpace, 0x8, 0x0E) },
                {KeyName.Tab, new Key(KeyName.Tab, 0x9, 0x0F) },
                {KeyName.Q, new Key(KeyName.Q, 0x51, 0x10) },
                {KeyName.W, new Key(KeyName.W, 0x57, 0x11) },
                {KeyName.E, new Key(KeyName.E, 0x45, 0x12) },
                {KeyName.R, new Key(KeyName.R, 0x52, 0x13) },
                {KeyName.T, new Key(KeyName.T, 0x54, 0x14) },
                {KeyName.Y, new Key(KeyName.Y, 0x59, 0x15) },
                {KeyName.U, new Key(KeyName.U, 0x55, 0x16) },
                {KeyName.I, new Key(KeyName.I, 0x49, 0x17) },
                {KeyName.O, new Key(KeyName.O, 0x4f, 0x18) },
                {KeyName.P, new Key(KeyName.P, 0x50, 0x19) },
                {KeyName.LBRACKET, new Key(KeyName.LBRACKET, 0xDB, 0x1A) },
                {KeyName.RBRACKET, new Key(KeyName.RBRACKET, 0xDD, 0x1B) },
                {KeyName.Enter, new Key(KeyName.Enter, 0xD, 0x1C) },
                {KeyName.LControl, new Key(KeyName.LControl, 0xA2, 0x1D) },
                {KeyName.A, new Key(KeyName.A, 0x41, 0x1E) },
                {KeyName.S, new Key(KeyName.S, 0x53, 0x1F) },
                {KeyName.D, new Key(KeyName.D, 0x44, 0x20) },
                {KeyName.F, new Key(KeyName.F, 0x46, 0x21) },
                {KeyName.G, new Key(KeyName.G, 0x47, 0x22) },
                {KeyName.H, new Key(KeyName.H, 0x48, 0x23) },
                {KeyName.J, new Key(KeyName.J, 0x4a, 0x24) },
                {KeyName.K, new Key(KeyName.K, 0x4b, 0x25) },
                {KeyName.L, new Key(KeyName.L, 0x4c, 0x26) },
                {KeyName.Semicolon, new Key(KeyName.Semicolon, 0xba, 0x27) },
                {KeyName.APOSTROPHE, new Key(KeyName.APOSTROPHE, 0xDE, 0x28) },
                {KeyName.Tilde, new Key(KeyName.APOSTROPHE, 0xC0, 0x29) },
                {KeyName.LShift, new Key(KeyName.LShift, 0xA0, 0x2A) },
                {KeyName.Backslash, new Key(KeyName.Backslash, 0xE2, 0x2B) },
                {KeyName.Z, new Key(KeyName.Z, 0x5A, 0x2C) },
                {KeyName.X, new Key(KeyName.X, 0x58, 0x2D) },
                {KeyName.C, new Key(KeyName.C, 0x43, 0x2E) },
                {KeyName.V, new Key(KeyName.V, 0x56, 0x2F) },
                {KeyName.B, new Key(KeyName.B, 0x42, 0x30) },
                {KeyName.N, new Key(KeyName.N, 0x4e, 0x31) },
                {KeyName.M, new Key(KeyName.M, 0x4d, 0x32) },
                {KeyName.Comma, new Key(KeyName.Comma, 0xBC, 0x33) },
                {KeyName.Dot, new Key(KeyName.Dot, 0xBE, 0x34) },
                {KeyName.Slash, new Key(KeyName.Slash, 0xBF, 0x35) },
                {KeyName.RShift, new Key(KeyName.RShift, 0xA1, 0x36) },
                {KeyName.Multiply, new Key(KeyName.Multiply, 0x6A, 0x37) },
                {KeyName.LAlt, new Key(KeyName.LAlt, 0xA4, 0x38) },
                {KeyName.Space, new Key(KeyName.Space, 0x20, 0x39) },
                {KeyName.CapsLock, new Key(KeyName.CapsLock, 0x14, 0x3A) },
                {KeyName.F1, new Key(KeyName.F1, 0x70, 0x3B) },
            };
        }
    }
}
