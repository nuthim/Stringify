namespace Stringify.Converters
{
    public class SByteConverter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return sbyte.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((sbyte)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}
