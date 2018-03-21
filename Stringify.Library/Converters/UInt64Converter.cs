using System.Globalization;

namespace Stringify.Converters
{
    public class UInt64Converter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return ulong.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}