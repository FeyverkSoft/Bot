using System;
using System.Runtime.Serialization;
using Core.Helpers;

namespace Core.ConfigEntity.ActionObjects
{
    /// <summary>
    /// Базовый класс для объектов действия
    /// </summary>
    public class BaseActionObject : IAction
    {
        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override String ToString()
        {
            return this.GetString();
        }
    }
}
