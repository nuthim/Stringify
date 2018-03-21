using System.Globalization;

namespace Stringify.Converters
{
    public class DecimalConverter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return decimal.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}