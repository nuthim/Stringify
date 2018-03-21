using System.Globalization;

namespace Stringify.Converters
{
    public class SingleConverter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return float.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}