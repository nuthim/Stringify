using System.Globalization;

namespace Stringify.Converters
{
    public class UInt32Converter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return uint.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}