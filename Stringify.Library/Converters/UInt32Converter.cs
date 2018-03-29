namespace Stringify.Converters
{
    public class UInt32Converter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return uint.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((uint)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}