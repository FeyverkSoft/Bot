using System;
using System.ComponentModel;

namespace Core.Core
{
    /// <summary>
    /// Коды клавиш, честно спи с MSDN
    /// </summary>
    [Description("Коды клавиш")]
    public enum KeyCode : Byte
    {
        [Description("A")]
        A = 0x41,
        [Description("Add")]
        Add = 0x6b,
        [Description("Alt")]
        Alt = 0x12,
        [Description("Apps")]
        Apps = 0x5d,
        [Description("Attn")]
        Attn = 0xf6,
        [Description("B")]
        B = 0x42,
        [Description("Back")]
        Back = 8,
        [Description("BrowserBack")]
        BrowserBack = 0xa6,
        [Description("BrowserFavorites")]
        BrowserFavorites = 0xab,
        [Description("BrowserForward")]
        BrowserForward = 0xa7,
        [Description("BrowserHome")]
        BrowserHome = 0xac,
        [Description("BrowserRefresh")]
        BrowserRefresh = 0xa8,
        [Description("BrowserSearch")]
        BrowserSearch = 170,
        [Description("BrowserStop")]
        BrowserStop = 0xa9,
        [Description("C")]
        C = 0x43,
        [Description("Cancel")]
        Cancel = 3,
        [Description("Capital")]
        Capital = 20,
        [Description("CapsLock")]
        CapsLock = 20,
        [Description("Clear")]
        Clear = 12,
        [Description("LControl")]
        LControl = 0xA2,
        [Description("RControl")]
        RControl = 0xA3,
        [Description("ControlKey")]
        ControlKey = 0x11,
        [Description("Crsel")]
        Crsel = 0xf7,
        [Description("D")]
        D = 0x44,
        [Description("D0")]
        D0 = 0x30,
        [Description("D1")]
        D1 = 0x31,
        [Description("D2")]
        D2 = 50,
        [Description("D3")]
        D3 = 0x33,
        [Description("D4")]
        D4 = 0x34,
        [Description("D5")]
        D5 = 0x35,
        [Description("D6")]
        D6 = 0x36,
        [Description("D7")]
        D7 = 0x37,
        [Description("D8")]
        D8 = 0x38,
        [Description("D9")]
        D9 = 0x39,
        [Description("Decimal")]
        Decimal = 110,
        [Description("Delete")]
        Delete = 0x2e,
        [Description("Divide")]
        Divide = 0x6f,
        [Description("Down")]
        Down = 40,
        [Description("E")]
        E = 0x45,
        [Description("End")]
        End = 0x23,
        [Description("Enter")]
        Enter = 13,
        [Description("EraseEof")]
        EraseEof = 0xf9,
        [Description("Escape")]
        Escape = 0x1b,
        [Description("Execute")]
        Execute = 0x2b,
        [Description("Exsel")]
        Exsel = 0xf8,
        [Description("F")]
        F = 70,
        [Description("F1")]
        F1 = 0x70,
        [Description("F10")]
        F10 = 0x79,
        [Description("F11")]
        F11 = 0x7a,
        [Description("F12")]
        F12 = 0x7b,
        [Description("F13")]
        F13 = 0x7c,
        [Description("F14")]
        F14 = 0x7d,
        [Description("F15")]
        F15 = 0x7e,
        [Description("F16")]
        F16 = 0x7f,
        [Description("F17")]
        F17 = 0x80,
        [Description("F18")]
        F18 = 0x81,
        [Description("F19")]
        F19 = 0x82,
        [Description("F2")]
        F2 = 0x71,
        [Description("F20")]
        F20 = 0x83,
        [Description("F21")]
        F21 = 0x84,
        [Description("F22")]
        F22 = 0x85,
        [Description("F23")]
        F23 = 0x86,
        [Description("F24")]
        F24 = 0x87,
        [Description("F3")]
        F3 = 0x72,
        [Description("F4")]
        F4 = 0x73,
        [Description("F5")]
        F5 = 0x74,
        [Description("F6")]
        F6 = 0x75,
        [Description("F7")]
        F7 = 0x76,

        [Description("F8")]
        F8 = 0x77,

        [Description("F9")]
        F9 = 0x78,

        [Description("FinalMode")]
        FinalMode = 0x18,

        [Description("G")]
        G = 0x47,

        [Description("H")]
        H = 0x48,

        [Description("HanguelMode")]
        HanguelMode = 0x15,

        [Description("HangulMode")]
        HangulMode = 0x15,

        [Description("HanjaMode")]
        HanjaMode = 0x19,

        [Description("Help")]
        Help = 0x2f,

        [Description("Home")]
        Home = 0x24,

        [Description("I")]
        I = 0x49,

        [Description("IMEAccept")]
        IMEAccept = 30,

        [Description("IMEAceept")]
        IMEAceept = 30,

        [Description("IMEConvert")]
        IMEConvert = 0x1c,

        [Description("IMEModeChange")]
        IMEModeChange = 0x1f,

        [Description("IMENonconvert")]
        IMENonconvert = 0x1d,

        [Description("Insert")]
        Insert = 0x2d,

        [Description("J")]
        J = 0x4a,

        [Description("JunjaMode")]
        JunjaMode = 0x17,

        [Description("K")]
        K = 0x4b,

        [Description("KanaMode")]
        KanaMode = 0x15,

        [Description("KanjiMode")]
        KanjiMode = 0x19,

        [Description("L")]
        L = 0x4c,

        [Description("LaunchApplication1")]
        LaunchApplication1 = 0xb6,

        [Description("LaunchApplication2")]
        LaunchApplication2 = 0xb7,

        [Description("LaunchMail")]
        LaunchMail = 180,

        [Description("LButton")]
        LButton = 1,

        [Description("LControlKey")]
        LControlKey = 0xa2,

        [Description("Left")]
        Left = 0x25,

        [Description("LineFeed")]
        LineFeed = 10,

        [Description("LMenu")]
        LMenu = 0xa4,

        [Description("LShiftKey")]
        LShiftKey = 160,

        [Description("LWin")]
        LWin = 0x5b,

        [Description("M")]
        M = 0x4d,

        [Description("MButton")]
        MButton = 4,

        [Description("MediaNextTrack")]
        MediaNextTrack = 0xb0,

        [Description("MediaPlayPause")]
        MediaPlayPause = 0xb3,

        [Description("MediaPreviousTrack")]
        MediaPreviousTrack = 0xb1,

        [Description("MediaStop")]
        MediaStop = 0xb2,

        [Description("Menu")]
        Menu = 0x12,

        [Description("Multiply")]
        Multiply = 0x6a,

        [Description("N")]
        N = 0x4e,

        [Description("Next")]
        Next = 0x22,

        [Description("NoName")]
        NoName = 0xfc,

        [Description("None")]
        None = 0,

        [Description("NumLock")]
        NumLock = 0x90,

        [Description("NumPad0")]
        NumPad0 = 0x60,

        [Description("NumPad1")]
        NumPad1 = 0x61,

        [Description("NumPad2")]
        NumPad2 = 0x62,

        [Description("NumPad3")]
        NumPad3 = 0x63,

        [Description("NumPad4")]
        NumPad4 = 100,

        [Description("NumPad5")]
        NumPad5 = 0x65,

        [Description("NumPad6")]
        NumPad6 = 0x66,

        [Description("NumPad7")]
        NumPad7 = 0x67,

        [Description("NumPad8")]
        NumPad8 = 0x68,

        [Description("NumPad9")]
        NumPad9 = 0x69,

        [Description("O")]
        O = 0x4f,

        [Description("Oem1")]
        Oem1 = 0xba,

        [Description("Oem102")]
        Oem102 = 0xe2,

        [Description("Oem2")]
        Oem2 = 0xbf,

        [Description("Oem3")]
        Oem3 = 0xc0,

        [Description("Oem4")]
        Oem4 = 0xdb,

        [Description("Oem5")]
        Oem5 = 220,

        [Description("Oem6")]
        Oem6 = 0xdd,

        [Description("Oem7")]
        Oem7 = 0xde,

        [Description("Oem8")]
        Oem8 = 0xdf,

        [Description("OemBackslash")]
        OemBackslash = 0xe2,

        [Description("OemClear")]
        OemClear = 0xfe,

        [Description("OemCloseBrackets")]
        OemCloseBrackets = 0xdd,

        [Description("Oemcomma")]
        Oemcomma = 0xbc,

        [Description("OemMinus")]
        OemMinus = 0xbd,

        [Description("OemOpenBrackets")]
        OemOpenBrackets = 0xdb,

        [Description("OemPeriod")]
        OemPeriod = 190,

        [Description("OemPipe")]
        OemPipe = 220,

        [Description("Oemplus")]
        Oemplus = 0xbb,

        [Description("OemQuestion")]
        OemQuestion = 0xbf,

        [Description("OemQuotes")]
        OemQuotes = 0xde,

        [Description("OemSemicolon")]
        OemSemicolon = 0xba,

        [Description("Oemtilde")]
        Oemtilde = 0xc0,

        [Description("P")]
        P = 80,

        [Description("Pa1")]
        Pa1 = 0xfd,

        [Description("Packet")]
        Packet = 0xe7,

        [Description("PageDown")]
        PageDown = 0x22,

        [Description("PageUp")]
        PageUp = 0x21,

        [Description("Pause")]
        Pause = 0x13,

        [Description("Play")]
        Play = 250,

        [Description("Print")]
        Print = 0x2a,

        [Description("PrintScreen")]
        PrintScreen = 0x2c,

        [Description("Prior")]
        Prior = 0x21,

        [Description("ProcessKey")]
        ProcessKey = 0xe5,

        [Description("Q")]
        Q = 0x51,

        [Description("R")]
        R = 0x52,

        [Description("RButton")]
        RButton = 2,

        [Description("RControlKey")]
        RControlKey = 0xa3,

        [Description("Return")]
        Return = 13,

        [Description("Right")]
        Right = 0x27,

        [Description("RMenu")]
        RMenu = 0xa5,

        [Description("RShiftKey")]
        RShiftKey = 0xa1,

        [Description("RWin")]
        RWin = 0x5c,

        [Description("S")]
        S = 0x53,

        [Description("Scroll")]
        Scroll = 0x91,

        [Description("Select")]
        Select = 0x29,

        [Description("SelectMedia")]
        SelectMedia = 0xb5,

        [Description("Separator")]
        Separator = 0x6c,

        [Description("LShift")]
        LShift = 0xA0,

        [Description("RShift")]
        RShift = 0xA1,

        [Description("ShiftKey")]
        ShiftKey = 0x10,

        [Description("Sleep")]
        Sleep = 0x5f,

        [Description("Snapshot")]
        Snapshot = 0x2c,

        [Description("Space")]
        Space = 0x20,

        [Description("Subtract")]
        Subtract = 0x6d,

        [Description("T")]
        T = 0x54,

        [Description("Tab")]
        Tab = 9,

        [Description("U")]
        U = 0x55,

        [Description("Up")]
        Up = 0x26,

        [Description("V")]
        V = 0x56,

        [Description("VolumeDown")]
        VolumeDown = 0xae,

        [Description("VolumeMute")]
        VolumeMute = 0xad,

        [Description("VolumeUp")]
        VolumeUp = 0xaf,

        [Description("W")]
        W = 0x57,

        [Description("X")]
        X = 0x58,

        [Description("XButton1")]
        XButton1 = 5,

        [Description("XButton2")]
        XButton2 = 6,

        [Description("Y")]
        Y = 0x59,

        [Description("Z")]
        Z = 90,

        [Description("Zoom")]
        Zoom = 0xfb,
    }
}
