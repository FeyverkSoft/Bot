namespace Core.ConfigEntity.ActionObjects
{
    public enum Conditional
    {
        /// <summary>
        /// Равно
        /// </summary>
        Equal,
        /// <summary>
        /// Не равно
        /// </summary>
        NotEqual,
        /// <summary>
        /// Больше
        /// </summary>
        IsGreaterThan,
        /// <summary>
        /// Меньше
        /// </summary>
        IsLessThan,
        /// <summary>
        ///Меньше или равно
        /// </summary>
        IsLessThanOrEqual,
        /// <summary>
        /// Больше или равно
        /// </summary>
        IsGreaterThanOrEqual
    }
}