using System.Globalization;

namespace Stringify.Converters
{
    public class ByteConverter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return byte.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}