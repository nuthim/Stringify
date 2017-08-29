using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;


namespace Stringify.Library
{
    public class StringConverter
    {
        public T ConvertTo<T>(string value)
        {
            var type = typeof(T);
            if (value == null && type.IsClass)
                return default(T);

            if (!IsEnumerableType(type))
                return Convert<T>(value);

            var elementType = type.IsGenericType ? type.GetGenericArguments() : new[] { type.GetElementType() };
            MethodInfo method = typeof(StringConverter).GetMethod("GetArray", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo generic = method.MakeGenericMethod(elementType);

            var result = generic.Invoke(this, new object[] { value, ',' });
            if (type.IsArray || type.IsInterface)
                return (T)result;

            return (T)Activator.CreateInstance(type, result);
        }

        public string ConvertFrom<T>(T value)
        {
            if (value == null)
                return null;

            var type = value.GetType();
            if (!IsEnumerableType(type))
                return Convert<T>(value);

            var genericArguments = type.IsGenericType ? type.GetGenericArguments() : new[] { type.GetElementType() };
            var methodInfo = typeof(StringConverter).GetMethod("AsString", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(genericArguments);
            return methodInfo.Invoke(this, new object[] { value, "," }) as string;
        }


        #region Helper Methods

        private T Convert<T>(string value)
        {
            var type = typeof(T);
            var converter = ConverterRegister.GetTypeConverter(type) ?? TypeDescriptor.GetConverter(type);

            if (typeof(TypeConverter) == converter.GetType())
            {
                T result;
                var deserializer = new JsonDeserializer();
                if (deserializer.Deserialize<T>(value, out result))
                    return result;
            }

            try
            {
                return (T)converter.ConvertFrom(value);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }

        private string Convert<T>(T value)
        {
            var type = typeof(T);
            var converter = ConverterRegister.GetTypeConverter(type) ?? TypeDescriptor.GetConverter(type);

            try
            {
                return converter.ConvertTo(value, typeof(string)) as string;
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message);
            }
        }

        private T[] GetArray<T>(string delimitedString, char delimiter = ',')
        {
            return delimitedString.Split(delimiter).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert<T>(x)).ToArray();
        }

        private string AsString<T>(IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable.Select(x => Convert(x)));
        }

        private static bool IsEnumerableType(Type type)
        {
            return type != null && type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
        }

        #endregion
    }
}
