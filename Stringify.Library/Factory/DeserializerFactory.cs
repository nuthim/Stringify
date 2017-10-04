using Stringify.Deserializers;

namespace Stringify
{
    internal static class DeserializerFactory
    {
        public static IDeserializer GetDeserializer(ConverterOptions options)
        {
            switch (options.StringFormat)
            {
                case Format.Json:
                    return new JsonDeserializer(options.JsonSettings);

                case Format.Xml:
                    return new XmlDeserializer(options.XmlSettings);

                default:
                    return null;
            }
        }
    }
}
