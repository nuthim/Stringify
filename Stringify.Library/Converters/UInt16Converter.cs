namespace Stringify.Converters
{
    public class UInt16Converter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return ushort.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((ushort)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}