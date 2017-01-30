using System;
using System.Collections.Generic;
using Core.Handlers.KeyBoard;
using Core.Handlers.KeyBoard.DxInput;
using Core.Handlers.KeyBoard.Native;
using Core.Handlers.KeyBoard.SendInput;

namespace Core.Handlers.Factory
{
    public static class KeyBoardHandlFactory
    {
        private static readonly Dictionary<KeyBoardType, IKeyBoard> Kb = new Dictionary<KeyBoardType, IKeyBoard>();
        /// <summary>
        /// если не указанно явно, то смотрим в конфиг
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IKeyBoard GetKeyBoard(KeyBoardType? type = null)
        {
            var t = type ?? AppConfig.Instance.KeyBoardType;
            switch (t)
            {
                case KeyBoardType.Native:
                    if (!Kb.ContainsKey(t))
                        Kb.Add(t, new NativeKeyBoard());
                    return Kb[t];
                case KeyBoardType.DxInput:
                    if (!Kb.ContainsKey(t))
                        Kb.Add(t, new DxInputKeyBoard());
                    return Kb[t];
                case KeyBoardType.SendInput:
                    if (!Kb.ContainsKey(t))
                        Kb.Add(t, new SendInputKeyBoard());
                    return Kb[t];
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
