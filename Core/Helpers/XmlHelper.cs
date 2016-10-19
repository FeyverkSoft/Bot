using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Core.Helpers
{
    public static class XmlHelper
    {
        /// <summary>
        /// Серелизовать объет в xml строку
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToXml(this object input)
        {
            if (input.GetType().Name.Contains("AnonymousType"))
            {
                return $"<?xml version=\"1.0\" encoding=\"utf - 8\"?>{Environment.NewLine}{input.ToXml(null)}";
            }
            return ToXmlInternal(input);
        }

        /// <summary>
        /// Серелизовать объет в xml строку
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static String ToXmlInternal(this Object obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                XmlWriter xml = new XmlTextWriter(stream, new UTF8Encoding());
                serializer.Serialize(xml, obj);
                stream.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch
            {
                return $"<?xml version=\"1.0\" encoding=\"utf - 8\"?>{Environment.NewLine}{obj.ToXml(obj.GetType().Name)}";
            }
        }

        private static readonly Type[] WriteTypes = {
        typeof(string), typeof(DateTime), typeof(Enum),
        typeof(decimal), typeof(Guid)
    };
        /// <summary>
        /// Это объект простого типа?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Boolean IsSimpleType(Type type)
        {
            return type.IsPrimitive || WriteTypes.Contains(type) || type.IsEnum || type.IsValueType;
        }
        /// <summary>
        /// Это перечисление?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Boolean IsEnumerable(Type type)
        {
            return (type.GetInterface("IEnumerable") != null);
        }


        private static XElement ToXml(this object input, string element)
        {
            if (input == null)
                return null;

            if (String.IsNullOrEmpty(element))
                element = "AnonymousTypeObject";
            element = XmlConvert.EncodeName(element);
            var ret = new XElement(element);

            var type = input.GetType();
            var props = type.GetProperties();

            if (!IsSimpleType(type) && IsEnumerable(type))
            {
                // singularize the element name
                if (element.EndsWith("ies"))
                    element = element.Substring(0, element.Length - 3) + "y";
                else if (element.EndsWith("s") && element.Length > 1)
                    element = element.Substring(0, element.Length - 1);
                var enumerable = input as IEnumerable;
                if (enumerable != null)
                    foreach (var item in enumerable)
                    {
                        ret.Add(IsSimpleType(item.GetType())
                            ? new XElement(element, item)
                            : item.ToXml(element));
                    }
            }
            else
            {
                var elements = from prop in props
                               let name = XmlConvert.EncodeName(prop.Name)
                               let val = prop.GetValue(input, null)
                               let value = IsSimpleType(prop.PropertyType)
                                       ? new XElement(name, val)
                                       : val.ToXml(name)
                               where value != null
                               select value;
                ret.Add(elements);
            }
            return ret;
        }
    }
}
