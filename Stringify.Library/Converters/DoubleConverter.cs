using System.Globalization;

namespace Stringify.Converters
{
    public class DoubleConverter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return double.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}