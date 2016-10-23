using Core.Attributes;
using Core.Helpers;

namespace Core.ConfigEntity.ActionObjects
{
    [LocDescription("MockAction")]
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
