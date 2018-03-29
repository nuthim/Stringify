namespace Stringify.Converters
{
    public class Int64Converter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return long.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((long)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}