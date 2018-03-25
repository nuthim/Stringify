using System.Globalization;

namespace Stringify.Converters
{
    public class SingleConverter : DecimalNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            var result = base.FromString(value, formatInfo);
            if (result != null)
                return (float)(int)result;

            return float.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}