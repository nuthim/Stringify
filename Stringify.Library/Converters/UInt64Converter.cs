namespace Stringify.Converters
{
    public class UInt64Converter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return ulong.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((ulong)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}