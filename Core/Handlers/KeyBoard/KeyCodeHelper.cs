﻿using System;
using System.Collections.Generic;
using Core.Core;

namespace Core.Handlers.KeyBoard
{
    /// <summary>
    /// Коды клавиш, честно спи с MSDN
    /// Надо переименовать в хелпер
    /// </summary>
    public static class KeyCodeHelper
    {
        private static readonly Dictionary<KeyName, Key> KeyArray;

        static KeyCodeHelper()
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
                {KeyName.Minus, new Key(KeyName.Minus, 0xbd, 0x0C) },
                {KeyName.Equals, new Key(KeyName.Equals, 0xBB, 0x0D) },
                {KeyName.Plus, new Key(KeyName.Plus, 0xBB, 0x0D) },
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
                {KeyName.Lbracket, new Key(KeyName.Lbracket, 0xDB, 0x1A) },
                {KeyName.Rbracket, new Key(KeyName.Rbracket, 0xDD, 0x1B) },
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
                {KeyName.Apostrophe, new Key(KeyName.Apostrophe, 0xDE, 0x28) },
                {KeyName.Tilde, new Key(KeyName.Apostrophe, 0xC0, 0x29) },
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
                {KeyName.F2, new Key(KeyName.F2, 0x71, 0x3C) },
                {KeyName.F3, new Key(KeyName.F3, 0x72, 0x3D) },
                {KeyName.F4, new Key(KeyName.F4, 0x73, 0x3E) },
                {KeyName.F5, new Key(KeyName.F5, 0x74, 0x3F) },
                {KeyName.F6, new Key(KeyName.F6, 0x75, 0x40) },
                {KeyName.F7, new Key(KeyName.F7, 0x76, 0x41) },
                {KeyName.F8, new Key(KeyName.F8, 0x77, 0x42) },
                {KeyName.F9, new Key(KeyName.F9, 0x78, 0x43) },
                {KeyName.F10, new Key(KeyName.F10, 0x79, 0x44) },
                {KeyName.NumLock, new Key(KeyName.NumLock, 0x90, 0x45) },
                {KeyName.ScrollLock, new Key(KeyName.ScrollLock, 0x91, 0x46) },
                {KeyName.NumPad7, new Key(KeyName.NumPad7, 0x67, 0x47) },
                {KeyName.NumPad8, new Key(KeyName.NumPad8, 0x68, 0x48) },
                {KeyName.NumPad9, new Key(KeyName.NumPad9, 0x69, 0x49) },
                {KeyName.NumPadMinus, new Key(KeyName.NumPadMinus, 0x6D, 0x4A) },
                {KeyName.NumPad4, new Key(KeyName.NumPad4, 0x64, 0x4B) },
                {KeyName.NumPad5, new Key(KeyName.NumPad5, 0x65, 0x4C) },
                {KeyName.NumPad6, new Key(KeyName.NumPad6, 0x66, 0x4D) },
                {KeyName.NumPadPlus, new Key(KeyName.NumPadPlus, 0x6B, 0x4E) },
                {KeyName.NumPad1, new Key(KeyName.NumPad1, 0x61, 0x4F) },
                {KeyName.NumPad2, new Key(KeyName.NumPad2, 0x62, 0x50) },
                {KeyName.NumPad3, new Key(KeyName.NumPad3, 0x63, 0x51) },
                {KeyName.NumPad0, new Key(KeyName.NumPad0, 0x60, 0x52) },
                {KeyName.NumPadDecimal, new Key(KeyName.NumPadDecimal, 0x6E, 0x53) },
                {KeyName.F11, new Key(KeyName.F11, 0x7a, 0x57) },
                {KeyName.F12, new Key(KeyName.F12, 0x7b, 0x58) },
                {KeyName.F13, new Key(KeyName.F13, 0x7c, 0x64) },
                {KeyName.F14, new Key(KeyName.F14, 0x7d, 0x65) },
                {KeyName.F15, new Key(KeyName.F15, 0x7e, 0x66) },
                {KeyName.NumPadEnter, new Key(KeyName.NumPadEnter, 0x0D, 0x9C) },
                {KeyName.RControl, new Key(KeyName.RControl, 0xA3, 0x9D) },
                {KeyName.NumPadDivide, new Key(KeyName.NumPadDivide, 0x6F, 0xB5) },
                {KeyName.PrintScreen, new Key(KeyName.PrintScreen, 0x2C, 0xB7) },
                {KeyName.RAlt, new Key(KeyName.RAlt, 0xA5, 0xB8) },
                {KeyName.Pause, new Key(KeyName.Pause, 0x13, 0xC5) },
                {KeyName.Home, new Key(KeyName.Home, 0x24, 0xC7) },
                {KeyName.Up, new Key(KeyName.Up, 0x26, 0xC8) },
                {KeyName.PageUp, new Key(KeyName.PageUp, 0x21, 0xC9) },
                {KeyName.Left, new Key(KeyName.Left, 0x25, 0xCB) },
                {KeyName.Right, new Key(KeyName.Right, 0x27, 0xCD) },
                {KeyName.End, new Key(KeyName.End, 0x23, 0xCF) },
                {KeyName.Down, new Key(KeyName.Down, 0x28, 0xD1) },
                {KeyName.Insert, new Key(KeyName.Insert, 0x2d, 0xD2) },
                {KeyName.Delete, new Key(KeyName.Delete, 0x2e, 0xD3) },
                {KeyName.LWindows, new Key(KeyName.LWindows, 0x5b, 0xDB) },
                {KeyName.RWindows, new Key(KeyName.RWindows, 0x5c, 0xDC) },
                {KeyName.Menu, new Key(KeyName.Menu, 0x12, 0xDD) }
            };
        }

        public static Key GetKeyInfo(this KeyName key)
        {
            return KeyArray[key];
        }

        public static UInt16 GetVKeyCode(this KeyName key)
        {
            return KeyArray[key].VKey;
        }
        public static UInt16 GetDxCode(this KeyName key)
        {
            return KeyArray[key].DxInputKey;
        }
    }
}
