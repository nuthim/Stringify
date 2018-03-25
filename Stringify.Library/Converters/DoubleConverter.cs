using System.Globalization;

namespace Stringify.Converters
{
    public class DoubleConverter : DecimalNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            var result = base.FromString(value, formatInfo);
            if (result != null)
                return (double)(int)result;

            return double.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}