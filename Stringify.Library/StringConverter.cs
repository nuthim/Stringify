using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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
        /// <returns>String</returns>
        public string ConvertFrom<T>(T value)
        {
            return ConvertFrom(value, ConverterOptions.Default);
        }

        /// <summary>
        /// Convert a given type to string
        /// </summary>
        /// <typeparam name="T">Type of input</typeparam>
        /// <param name="value">Instance of input type</param>
        /// <param name="options">Converter options to customise</param>
        /// <returns>String</returns>
        public string ConvertFrom<T>(T value, ConverterOptions options)
        {
            if (value == null)
                return null;

            if (options == null)
                options = ConverterOptions.Default;

            var type = value.GetType();
            if (!EnumerableHelper.IsEnumerableType(type))
                return Convert(value, options);

            return InvokeAsString(type, value, options) as string;
        }


        #region Helper Methods

        private static T Convert<T>(string value, ConverterOptions options)
        {
            var type = typeof(T);
            
            try
            {
                var converter = TypeConverterFactory.GetTypeConverter(type, options) ?? TypeDescriptor.GetConverter(type);
                return (T)converter.ConvertFrom(null, options.CultureInfo, value);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }

        private static string Convert<T>(T value, ConverterOptions options)
        {
            var type = typeof(T);
            var converter = TypeConverterFactory.GetTypeConverter(type, options) ?? TypeDescriptor.GetConverter(type);

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
            var elementType = enumerableType.IsGenericType ? enumerableType.GetGenericArguments() : new[] { enumerableType.GetElementType() ?? typeof(object) };
            var method = typeof(StringConverter).GetMethod("GetArray", BindingFlags.NonPublic | BindingFlags.Instance);
            var generic = method.MakeGenericMethod(elementType);
            return generic.Invoke(this, new object[] { value, options });
        }

        // ReSharper disable once UnusedMember.Local
        private T[] GetArray<T>(string delimitedString, ConverterOptions options)
        {
            return delimitedString.Split(options.Delimiter).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert<T>(x, options)).ToArray();
        }

        private object InvokeAsString(Type enumerableType, object value, ConverterOptions options)
        {
            var genericArguments = enumerableType.IsGenericType ? enumerableType.GetGenericArguments() : new[] { enumerableType.GetElementType() };
            var methodInfo = typeof(StringConverter).GetMethod("AsString", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(genericArguments);
            return methodInfo.Invoke(this, new [] { value, options });
        }

        // ReSharper disable once UnusedMember.Local
        private string AsString<T>(IEnumerable<T> enumerable, ConverterOptions options)
        {
            return string.Join(options.Delimiter.ToString(), enumerable.Select( x=> Convert(x, options)));
        }

        #endregion
    }
}
