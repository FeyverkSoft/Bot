using System;
using System.IO;

namespace Core.ConfigEntity
{
    public interface IConfigReader<TConfigType> where TConfigType : class, new()
    {
        /// <summary>
        /// Загрузить конфиг 
        /// </summary>
        /// <returns></returns>
        TConfigType Load(String path);

        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        /// <param name="conf"></param>
        /// <param name="dir">Путь для сохранения конфига</param>
        TConfigType Save(TConfigType conf, String dir);

        /// <summary>
        /// Загрузить конфиг 
        /// </summary>
        /// <returns></returns>
        TConfigType Load(StreamReader stream);
    }
}