

namespace Stringify.Converters
{
    public class ByteConverter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return byte.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((byte) value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}