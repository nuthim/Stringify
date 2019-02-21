using System;
using System.Collections;
using System.Reflection;

namespace Stringify
{
    internal static class EnumerableHelper
    {
        public static bool IsEnumerableType(Type type)
        {
            return type != null && type != typeof(string) && typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
        }

    }
}
