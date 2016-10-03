using System;
using System.IO;
using Core.Helpers;
using System.Reflection;
using LogWrapper;

namespace Core.ConfigEntity
{
    public sealed class ConfigReader : IConfigReader
    {
        private readonly String _directory;
        public ConfigReader(String dir)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(dir: {dir});");

            if (String.IsNullOrEmpty(dir))
                throw new ArgumentNullException(nameof(dir));
            _directory = dir;
        }
        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        /// <param name="conf"></param>
        public void Save(Config conf)
        {
            try
            {
                Log.WriteLine($"{GetType().Name}.{nameof(Save)}->(conf: {(conf == null ? "not null" : " null")})");
                if (conf == null)
                    throw new ArgumentNullException(nameof(conf));
                using (StreamWriter sw = new StreamWriter(_directory))
                {
                    sw.Write(conf.ToJson());
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex, LogLevel.Error);
                throw;
            }

        }
        /// <summary>
        /// Загрузить конфиг 
        /// </summary>
        /// <returns></returns>
        public Config Load()
        {
            try
            {
                Log.WriteLine($"{GetType().Name}.{nameof(Load)}->(dir: {_directory})");
                var vers = Assembly.GetExecutingAssembly().GetName().Version;
                using (StreamReader sr = new StreamReader(_directory))
                {
                    var temp = sr.ParseJson<Config>();
                    if (temp.BotVer != new FileVersion(vers))
                    {
                        Log.WriteLine($"Не совпадают версии файла и приложения, возможны побочные эффекты!");
                        Log.WriteLine($"{temp.BotVer} != {vers}");
                    }
                    return temp;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex, LogLevel.Error);
                throw;
            }

        }
    }
}
