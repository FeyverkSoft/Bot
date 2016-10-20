using System;
using System.IO;
using Core.Helpers;
using LogWrapper;

namespace Core.ConfigEntity
{
    internal sealed class ConfigReader<TConfigType> : IConfigReader<TConfigType> where TConfigType : class, new()
    {
        private readonly Boolean _ignoreNull;
        private readonly Boolean _typeName;
        public ConfigReader(Boolean ignoreNull = true, Boolean loadPlugins = true, Boolean typeName = true)
        {
            Log.WriteLine($"{GetType().Name}.ctor->(ignoreNull: {ignoreNull}");
            _ignoreNull = ignoreNull;
            _typeName = typeName;

            if (loadPlugins)
                Assemblys.LoadPlugins();
        }

        /// <summary>
        /// Сохранить конфиг
        /// </summary>
        /// <param name="conf"></param>
        /// <param name="dir"></param>
        public TConfigType Save(TConfigType conf, String dir)
        {
            if (String.IsNullOrEmpty(dir))
                throw new ArgumentNullException(nameof(dir));
            try
            {
                Log.WriteLine($"{GetType().Name}.{nameof(Save)}->(conf: {(conf == null ? "not null" : " null")})");
                if (conf == null)
                    throw new ArgumentNullException(nameof(conf));
                using (var sw = new StreamWriter(dir))
                {
                    sw.Write(conf.ToJson(ignoreNull: _ignoreNull, typeName: _typeName));
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
        public TConfigType Load(String dir)
        {
            if (String.IsNullOrEmpty(dir))
                throw new ArgumentNullException(nameof(dir));
            try
            {
                Log.WriteLine($"{GetType().Name}.{nameof(Load)}->(dir: {dir})");
                using (var sr = new StreamReader(dir))
                {
                    return sr.ParseJson<TConfigType>();
                }
            }
            catch (FileNotFoundException)
            {
                return Save(new TConfigType(), dir);
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
        public TConfigType Load(StreamReader dir)
        {
            if (dir == null)
                throw new ArgumentNullException(nameof(dir));
            try
            {
                return dir.ParseJson<TConfigType>();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex, LogLevel.Error);
                throw;
            }
        }
    }
}
