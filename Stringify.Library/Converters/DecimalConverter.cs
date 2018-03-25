using System.Globalization;

namespace Stringify.Converters
{
    public class DecimalConverter : DecimalNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            var result = base.FromString(value, formatInfo);
            if (result != null)
                return (decimal)(int)result;

            return decimal.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}