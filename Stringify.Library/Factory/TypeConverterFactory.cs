using System;
using System.ComponentModel;
using System.Collections.Concurrent;


namespace Stringify.Factory
{
    public static class TypeConverterFactory
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> _converters = new ConcurrentDictionary<Type, TypeConverter>();

        static TypeConverterFactory()
        {
            RegisterTypeConverter(typeof(sbyte), new Converters.SByteConverter());
            RegisterTypeConverter(typeof(byte), new Converters.ByteConverter());
            RegisterTypeConverter(typeof(float), new Converters.SingleConverter());
            RegisterTypeConverter(typeof(decimal), new Converters.DecimalConverter());
            RegisterTypeConverter(typeof(double), new Converters.DoubleConverter());
            RegisterTypeConverter(typeof(short), new Converters.Int16Converter());
            RegisterTypeConverter(typeof(int), new Converters.Int32Converter());
            RegisterTypeConverter(typeof(long), new Converters.Int64Converter());
            RegisterTypeConverter(typeof(ushort), new Converters.UInt16Converter());
            RegisterTypeConverter(typeof(uint), new Converters.UInt32Converter());
            RegisterTypeConverter(typeof(ulong), new Converters.UInt64Converter());
            RegisterTypeConverter(typeof(object), new Converters.ObjectConverter());
            RegisterTypeConverter(typeof(bool), new Converters.LogicalBooleanConverter());
        }

        public static void RegisterTypeConverter(Type type, TypeConverter converter)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            
            _converters.AddOrUpdate(type, converter, (x, y) => converter);
        }

        public static TypeConverter GetTypeConverter(Type type, ConverterOptions options)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            TypeConverter converter;
            _converters.TryGetValue(type, out converter);
            var numberConverter = converter as Converters.BaseNumberConverter;
            if (numberConverter != null)
                numberConverter.Options = options;

            return converter;
        }
    }
}
