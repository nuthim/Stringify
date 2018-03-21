using System.Globalization;


namespace Stringify.Converters
{
    public class SByteConverter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return sbyte.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}
