using CommonLib.Attributes;
using Core.Resources;

namespace Core.ConfigEntity.ActionObjects
{
    public enum EConditional
    {
        /// <summary>
        /// Равно
        /// </summary>
        [LocDescription("Equal",typeof(CoreText))]
        Equal,
        /// <summary>
        /// Не равно
        /// </summary>
        [LocDescription("NotEqual", typeof(CoreText))]
        NotEqual,
        /// <summary>
        /// Больше
        /// </summary>
        [LocDescription("IsGreaterThan", typeof(CoreText))]
        IsGreaterThan,
        /// <summary>
        /// Меньше
        /// </summary>
        [LocDescription("IsLessThan", typeof(CoreText))]
        IsLessThan,
        /// <summary>
        ///Меньше или равно
        /// </summary>
        [LocDescription("IsLessThanOrEqual", typeof(CoreText))]
        IsLessThanOrEqual,
        /// <summary>
        /// Больше или равно
        /// </summary>
        [LocDescription("IsGreaterThanOrEqual", typeof(CoreText))]
        IsGreaterThanOrEqual
    }
}