using System;
using System.Collections;

namespace Stringify
{
    internal static class EnumerableHelper
    {
        public static bool IsEnumerableType(Type type)
        {
            return type != null && type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
        }

    }
}
