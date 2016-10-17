using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace WpfExecutor.Extensions.Localization
{
    public static class LocalizedStringsHelper
    {
        public static string SerializeToXml(string defaultValue, IDictionary<string, string> localizedValues)
        {
            try
            {
                var localizedStrings = new LocalizedStrings(defaultValue, localizedValues);
                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true
                };
                var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                namespaces.Add("", "http://www.walletone.com/");
                var serializer = new XmlSerializer(typeof(LocalizedStrings), "http://www.walletone.com/");
                using (var stream = new StringWriter())
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, localizedStrings, namespaces);

                    var result = stream.ToString();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public static LocalizedStrings DeserializeFromXml(string xml)
        {
            try
            {
                if (string.IsNullOrEmpty(xml))
                    return null;

                var serializer = new XmlSerializer(typeof(LocalizedStrings), "http://www.walletone.com/");
                using (var reader = new StringReader(xml))
                {
                    var localizedString = serializer.Deserialize(reader) as LocalizedStrings;
                    return localizedString;
                }
            }
            catch
            {
                return null;
            }
        }

        public static string GetLocalizedValue(string xml, CultureInfo culture)
        {
            var strings = DeserializeFromXml(xml);
            return GetLocalizedValue(strings, culture);
        }

        public static string GetLocalizedValue(string xml)
        {
            return GetLocalizedValue(xml, CultureInfo.CurrentCulture);
        }

        public static string GetLocalizedValue(LocalizedStrings strings, CultureInfo culture)
        {
            var stringValue =
                strings?.LocalizedValues?.FirstOrDefault(
                    x => culture.Equals(new CultureInfo(x.CultureId)))?.Value ??
                strings?.DefaultValue;
            if (string.IsNullOrEmpty(stringValue))
                return null;
            return stringValue;
        }
    }

    [XmlRoot("LocalizedString")]
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        public LocalizedStrings(string defaultValue, IDictionary<string, string> localizedValues)
        {
            DefaultValue = defaultValue;
            LocalizedValues = localizedValues.Select(x => new LocalizedValue(x.Key, x.Value)).ToList();
        }

        [XmlElement]
        public string DefaultValue { get; set; }

        [XmlElement("LocalizedValue")]
        public List<LocalizedValue> LocalizedValues { get; set; }
    }

    public class LocalizedValue
    {
        public LocalizedValue()
        {
        }

        public LocalizedValue(string cultureId, string value)
        {
            CultureId = cultureId;
            Value = value;
        }

        [XmlAttribute]
        public string CultureId { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
