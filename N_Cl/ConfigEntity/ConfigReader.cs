using System;
using System.IO;
using Core.Helpers;
using System.Diagnostics;
using System.Reflection;

namespace Core.ConfigEntity
{
    public sealed class ConfigReader : IConfigReader
    {
        private String directory;
        public ConfigReader(String dir)
        {
            Debug.WriteLine($"{GetType().Name}.ctor->(dir: {dir});");

            if (String.IsNullOrEmpty(dir))
                throw new ArgumentNullException(nameof(dir));
            directory = dir;
        }
        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        /// <param name="conf"></param>
        public void Save(Config conf)
        {
            Debug.WriteLine($"{GetType().Name}.{nameof(Save)}->(conf: {(conf == null ? "not null" : " null")})");
            if (conf == null)
                throw new ArgumentNullException(nameof(conf));
            using (StreamWriter sw = new StreamWriter(directory))
            {
                sw.Write(conf.ToJson());
                sw.Flush();
            }
        }
        /// <summary>
        /// Загрузить конфиг 
        /// </summary>
        /// <returns></returns>
        public Config Load()
        {
            Debug.WriteLine($"{GetType().Name}.{nameof(Load)}->(dir: {directory})");
            Config temp;
            var vers = Assembly.GetExecutingAssembly().GetName().Version;
            using (StreamReader sr = new StreamReader(directory))
            {
                temp = sr.ParseJson<Config>();
                if (temp.BotVer != new FileVersion(vers))
                {
                    Debug.WriteLine($"Не совпадают версии файла и приложения, возможны побочные эффекты!");
                    Debug.WriteLine($"{temp.BotVer} != {vers}");
                }
                return temp;
            }
        }
    }
}
