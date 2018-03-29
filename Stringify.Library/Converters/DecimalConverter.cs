namespace Stringify.Converters
{

    /// <summary>
    /// Provides a type converter to convert decimal objects to and from string representations.
    /// </summary>
    public class DecimalConverter : DecimalNumberConverter
    {
        internal override object FromString(string value)
        {
            var result = base.FromString(value);
            if (result != null)
                return (decimal)(int)result;

            return decimal.Parse(value, Options.NumberStyles , Options.CultureInfo.NumberFormat);
        }

        internal override string ToString(object value, string format)
        {
            return ((decimal)value).ToString(format, Options.CultureInfo.NumberFormat);
        }
    }
}