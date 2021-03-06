﻿

namespace Stringify.Converters
{
    /// <summary>
    /// Provides a type converter to convert double objects to and from string representations.
    /// </summary>
    public class DoubleConverter : DecimalNumberConverter
    {
        internal override object FromString(string value)
        {
            var result = base.FromString(value);
            if (result != null)
                return (double)(int)result;

            return double.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }
        internal override string ToString(object value, string format)
        {
            return ((double)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}