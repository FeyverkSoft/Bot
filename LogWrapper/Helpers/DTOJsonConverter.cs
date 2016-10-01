using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Core.Helpers
{
    class DTOJsonConverter : JsonConverter
    {
        private static readonly List<Type> Interfaces = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsInterface).ToList();
        private static readonly List<Type> Classes = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass).ToList();
        public override bool CanConvert(Type objectType)
        {
            if (Interfaces.Contains(objectType))
            {
                return true;
            }
            return false;
        }

        public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer)
        {
            foreach (var interf in Interfaces)
                if (objectType.FullName == interf.FullName)
                    foreach (var cla in Classes.Where(x => x.GetInterfaces().Contains(interf)))
                    {
                        var result = serializer.Deserialize(reader, cla);
                        if (result != null)
                            return result;
                    }
            throw new NotSupportedException($"Type {objectType} unexpected.");
        }

        public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
