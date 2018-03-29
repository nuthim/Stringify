namespace Stringify.Converters
{
    /// <summary>
    /// Provides a type converter to convert 8-bit signed integer objects to and from string representations.
    /// </summary>
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
