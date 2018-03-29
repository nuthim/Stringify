

namespace Stringify.Converters
{
    public class Int32Converter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return int.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((int)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}