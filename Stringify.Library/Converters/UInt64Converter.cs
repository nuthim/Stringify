namespace Stringify.Converters
{
    /// <summary>
    /// Provides a type converter to convert 64-bit signed integer objects to and from string representations.
    /// </summary>
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