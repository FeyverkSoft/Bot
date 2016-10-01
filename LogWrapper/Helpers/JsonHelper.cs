using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LogWrapper.Helpers
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
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Objects;
            serializer.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;
            serializer.NullValueHandling = NullValueHandling.Ignore;
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
            JsonSerializer serializer = new JsonSerializer();
            //serializer.Converters.Add(new DTOJsonConverter());
            serializer.TypeNameHandling = TypeNameHandling.Objects;
            serializer.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            return serializer.Deserialize<T>(new JsonTextReader(json));
        }



        /// <summary>
        /// Преобразовать объект в JSON, причём Enum будет записано текстом
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String ToJson(this Object obj, Boolean formatting = true, Boolean typeName = false)
        {
            return JsonConvert.SerializeObject(obj, formatting ? Formatting.Indented : Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
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
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            });
        }
    }
}
