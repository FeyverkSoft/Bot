namespace Executor.ConfigEntity
{
    public interface IConfigReader
    {
        /// <summary>
        /// Загрузить конфиг 
        /// </summary>
        /// <returns></returns>
        Config Load();
        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        /// <param name="conf"></param>
        void Save(Config conf);
    }
}