using System.Globalization;

namespace Stringify.Converters
{
    public class Int64Converter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return long.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}