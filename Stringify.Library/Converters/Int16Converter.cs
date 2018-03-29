namespace Stringify.Converters
{
    /// <summary>
    /// Provides a type converter to convert 16-bit signed integer objects to and from string representations.
    /// </summary>
    public class Int16Converter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            return short.Parse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((short)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}