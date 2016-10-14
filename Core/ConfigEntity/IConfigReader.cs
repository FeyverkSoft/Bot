namespace Core.ConfigEntity
{
    public interface IConfigReader<TConfigType> where TConfigType : class, new()
    {
        /// <summary>
        /// Загрузить конфиг 
        /// </summary>
        /// <returns></returns>
        TConfigType Load();
        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        /// <param name="conf"></param>
        TConfigType Save(TConfigType conf);
    }
}