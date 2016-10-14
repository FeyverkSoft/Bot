using System;
using System.IO;
using Core.Helpers;
using LogWrapper;

namespace Core.ConfigEntity
{
    public sealed class ConfigReader<TConfigType> : IConfigReader<TConfigType> where TConfigType : class, new()
    {
        private readonly String _directory;
        private readonly Boolean _ignoreNull;
        public ConfigReader(String dir, Boolean ignoreNull = true)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(dir: {dir}); ignoreNull: {ignoreNull}");
            _ignoreNull = ignoreNull;
            if (String.IsNullOrEmpty(dir))
                throw new ArgumentNullException(nameof(dir));
            _directory = dir;
            Assemblys.LoadPlugins();
        }
        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        /// <param name="conf"></param>
        public TConfigType Save(TConfigType conf)
        {
            try
            {
                Log.WriteLine($"{GetType().Name}.{nameof(Save)}->(conf: {(conf == null ? "not null" : " null")})");
                if (conf == null)
                    throw new ArgumentNullException(nameof(conf));
                using (var sw = new StreamWriter(_directory))
                {
                    sw.Write(conf.ToJson(ignoreNull: _ignoreNull));
                    sw.Flush();
                }
                return conf;
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
        public TConfigType Load()
        {
            try
            {
                Log.WriteLine($"{GetType().Name}.{nameof(Load)}->(dir: {_directory})");
                using (var sr = new StreamReader(_directory))
                {

                    return sr.ParseJson<TConfigType>();
                }
            }
            catch (FileNotFoundException)
            {
                return Save(new TConfigType());
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex, LogLevel.Error);
                throw;
            }

        }
    }
}
