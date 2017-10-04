using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Linq;

namespace Stringify.Deserializers
{

    internal class JsonDeserializer : IDeserializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializerSettings Settings { get; }

        public JsonDeserializer(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public object Deserialize(string value, Type destinationType)
        {
            if (value == null)
                return null;

            JSchema schema = new JSchemaGenerator().Generate(destinationType);
            if (EnumerableHelper.IsEnumerableType(destinationType))
            {
                if (JArray.Parse(value).IsValid(schema))
                    return JsonConvert.DeserializeObject(value, destinationType, _settings);
            }
            else
            {
                if (JObject.Parse(value).IsValid(schema))
                    return JsonConvert.DeserializeObject(value, destinationType, _settings);
            }

            throw new InvalidOperationException("");
        }
    }
}
