using System;
using System.ComponentModel;
using System.Collections.Concurrent;

namespace Stringify.Library
{
    public static class ConverterRegister
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> _converters = new ConcurrentDictionary<Type, TypeConverter>();

        public static void RegisterTypeConverter(Type type, TypeConverter converter)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            
            _converters.AddOrUpdate(type, converter, (x, y) => converter);
        }

        public static TypeConverter GetTypeConverter(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            TypeConverter converter;
            _converters.TryGetValue(type, out converter);
            return converter;
        }
    }


}
