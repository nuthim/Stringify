using System;


namespace Stringify.Deserializers
{
    public interface IDeserializer
    {
        object Deserialize(string value, Type destinationType);
    }
}
