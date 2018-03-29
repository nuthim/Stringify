using System;
using System.ComponentModel;
using System.Collections.Concurrent;


namespace Stringify.Factory
{
    /// <summary>
    /// 
    /// </summary>
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
            RegisterTypeConverter(typeof(DateTime), new Converters.DateTimeConverter());
        }

        /// <summary>
        /// Registers a type converter for the given type
        /// </summary>
        /// <param name="type">Type for which custom converter is to be registered</param>
        /// <param name="converter">Converter to register</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> or <paramref name="converter" /> is null.
        /// </exception>
        public static void RegisterTypeConverter(Type type, TypeConverter converter)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (converter == null)
                throw new ArgumentNullException(nameof(converter));

            _converters.AddOrUpdate(type, converter, (x, y) => converter);
        }

        /// <summary>
        /// Gets the registered <see cref="TypeConverter"/> associated with a given type. 
        /// Also, initializes it with the supplied <see cref="ConverterOptions"/> if it has been derived from <see cref="Stringify.Converters.ICustomConverter"/>
        /// </summary>
        /// <param name="type">Type for which registered converter is queried</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static TypeConverter GetTypeConverter(Type type, ConverterOptions options)
        {
            if (type == null)
                return null;

            TypeConverter converter;
            _converters.TryGetValue(type, out converter);
            var numberConverter = converter as Converters.ICustomConverter;
            if (numberConverter != null)
                numberConverter.Options = options;

            return converter;
        }
    }
}
