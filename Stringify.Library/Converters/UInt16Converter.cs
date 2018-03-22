using System.Globalization;

namespace Stringify.Converters
{
    public class UInt16Converter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return ushort.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}