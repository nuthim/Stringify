using System.Globalization;

namespace Stringify.Converters
{
    public class Int32Converter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return int.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}