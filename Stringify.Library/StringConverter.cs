using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;
using Stringify.Factory;

namespace Stringify
{
    public class StringConverter
    {
        /// <summary>
        /// Converts a string to the desired type using the default options <see cref="Stringify.ConverterOptions.Default"/>
        /// </summary>
        /// <typeparam name="T">Desired output type</typeparam>
        /// <param name="value">String value to be converted</param>
        /// <returns>Instance of the desired type</returns>
        public T ConvertTo<T>(string value)
        {
            return ConvertTo<T>(value, ConverterOptions.Default);
        }

        /// <summary>
        /// Converts a string to the desired type using the specified options
        /// </summary>
        /// <typeparam name="T">Desired output type</typeparam>
        /// <param name="value">String value to be converted</param>
        /// <param name="options">Converter options to customise</param>
        /// <returns>Instance of the desired type</returns>
        public T ConvertTo<T>(string value, ConverterOptions options)
        {
            var type = typeof(T);
            if (value == null && type.IsClass)
                return default(T);

            if (options == null)
                options = ConverterOptions.Default;

            if (!EnumerableHelper.IsEnumerableType(type))
                return Convert<T>(value, options);

            var enumerable = InvokeGetArray(type, value, options);
            if (type.IsArray || type.IsInterface)
                return (T)enumerable;

            return (T)Activator.CreateInstance(type, enumerable);
        }

        /// <summary>
        /// Convert a given type to string
        /// </summary>
        /// <typeparam name="T">Type of input</typeparam>
        /// <param name="value">Instance of input type</param>
        /// <param name="separator">Defaults to ',' Only needed when converting from enumerable types to a delimited string</param>
        /// <returns>String</returns>
        public string ConvertFrom<T>(T value, char separator = ',')
        {
            if (value == null)
                return null;

            var type = value.GetType();
            if (!EnumerableHelper.IsEnumerableType(type))
                return Convert<T>(value);

            return InvokeAsString(type, value, separator) as string;
        }


        #region Helper Methods

        private T Convert<T>(string value, ConverterOptions options)
        {
            var type = typeof(T);
            
            try
            {
                if (options.StringFormat != Format.None)
                {
                    var deserializer = DeserializerFactory.GetDeserializer(options);
                    if (deserializer != null)
                        return (T)deserializer.Deserialize(value, typeof(T));
                }

                var converter = TypeConverterFactory.GetTypeConverter(type) ?? TypeDescriptor.GetConverter(type);
                return (T)converter.ConvertFrom(null, CultureInfo.CurrentUICulture, value);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }

        private string Convert<T>(T value)
        {
            var type = typeof(T);
            var converter = TypeConverterFactory.GetTypeConverter(type) ?? TypeDescriptor.GetConverter(type);

            try
            {
                return converter.ConvertTo(value, typeof(string)) as string;
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }

        private object InvokeGetArray(Type enumerableType, string value, ConverterOptions options)
        {
            var elementType = enumerableType.IsGenericType ? enumerableType.GetGenericArguments() : new[] { enumerableType.GetElementType() };
            MethodInfo method = typeof(StringConverter).GetMethod("GetArray", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo generic = method.MakeGenericMethod(elementType);
            return generic.Invoke(this, new object[] { value, options });
        }

        private T[] GetArray<T>(string delimitedString, ConverterOptions options)
        {
            if (options.StringFormat != Format.None)
            {
                var deserializer = DeserializerFactory.GetDeserializer(options);
                if (deserializer != null)
                    return (T[])deserializer.Deserialize(delimitedString, typeof(T[]));
            }

            return delimitedString.Split(options.Delimiter).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert<T>(x, options)).ToArray();
        }

        private object InvokeAsString(Type enumerableType, object value, char separator)
        {
            var genericArguments = enumerableType.IsGenericType ? enumerableType.GetGenericArguments() : new[] { enumerableType.GetElementType() };
            var methodInfo = typeof(StringConverter).GetMethod("AsString", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(genericArguments);
            return methodInfo.Invoke(this, new object[] { value, separator });
        }

        private string AsString<T>(IEnumerable<T> enumerable, char separator)
        {
            return string.Join(separator.ToString(), enumerable.Select(x => Convert(x)));
        }

        #endregion
    }
}
