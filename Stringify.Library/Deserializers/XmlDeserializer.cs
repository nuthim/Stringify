using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Stringify.Deserializers
{

    internal class XmlDeserializer : IDeserializer
    {
        private readonly XmlReaderSettings _settings;

        public XmlReaderSettings Settings { get; }

        public XmlDeserializer(XmlReaderSettings settings)
        {
            _settings = settings;
        }

        public object Deserialize(string value, Type destinationType)
        {
            if (value == null)
                return null;

            var serializer = new XmlSerializer(destinationType);

            using (var reader = new StringReader(value))
            {
                using (var xmlReader = XmlReader.Create(reader, _settings))
                    return serializer.Deserialize(reader);
            }

            throw new InvalidOperationException("");
        }
    }
}
