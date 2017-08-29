using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Stringify.Library
{
    internal class JsonDeserializer
    {
        public bool Deserialize<T>(string value, out T result)
        {
            result = default(T);
            if (value == null)
                return false;

            JSchema schema = new JSchemaGenerator().Generate(typeof(T));
            try
            {
                if (JObject.Parse(value).IsValid(schema))
                {
                    result = JsonConvert.DeserializeObject<T>(value);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
