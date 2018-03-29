using System.Globalization;


namespace Stringify.Converters
{
    /// <summary>
    /// Provides a base type converter to convert decimal / double / float objects to and from string representations.
    /// </summary>
    public abstract class DecimalNumberConverter : BaseNumberConverter
    {
        internal override object FromString(string value)
        {
            if ((Options.NumberStyles & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
            {
                /*
                NumberStyles.AllowHexSpecifier is not supported on floating point numbers.
                On a best effort basis check if the number is integral. If yes, allow the conversion.
                */
                int number;
                if (int.TryParse(value, Options.NumberStyles, Options.CultureInfo.NumberFormat, out number))
                    return number;
            }

            return null;
        }
    }
}
