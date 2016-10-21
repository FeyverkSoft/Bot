using System.ComponentModel;
using Core.Helpers;

namespace Core.ConfigEntity.ActionObjects
{
    [Description("Фейковый класс")]
    public class MockAction : IAction
    {
        /// <summary>Возвращает строку, представляющую текущий объект.</summary>
        /// <returns>Строка, представляющая текущий объект.</returns>
        public override string ToString()
        {
            return this.GetString();
        }
    }
}
