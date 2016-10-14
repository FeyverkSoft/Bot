using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Преобразовать строку сожержащую JSON в объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ParseJson<T>(this String json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                NullValueHandling = NullValueHandling.Ignore,
                Binder = new MySerializationBinder()
            };
            return serializer.Deserialize<T>(new JsonTextReader(new StringReader(json)));
        }
        /// <summary>
        /// Преобразовать поток сожержащую JSON в объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ParseJson<T>(this StreamReader json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                NullValueHandling = NullValueHandling.Ignore,
                Binder = new MySerializationBinder()
            };
            //serializer.Converters.Add(new PluginConverter());
            return serializer.Deserialize<T>(new JsonTextReader(json));
        }


        /// <summary>
        /// Преобразовать объект в JSON, причём Enum будет записано текстом
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="formatting"></param>
        /// <param name="typeName"></param>
        /// <param name="ignoreNull"></param>
        /// <returns></returns>
        public static String ToJson(this Object obj, Boolean formatting = true, Boolean typeName = true, Boolean ignoreNull = true)
        {
            return JsonConvert.SerializeObject(obj, formatting ? Formatting.Indented : Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                TypeNameHandling = typeName ? TypeNameHandling.Objects : TypeNameHandling.None,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                }
            });
        }
        /// <summary>
        /// Приобразовать объект в JSON с датой по ISO стандарту
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ToJsonExtendedIsoDate(this Object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(),
                    new IsoDateTimeConverter(),
                },
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
        }
    }
}
