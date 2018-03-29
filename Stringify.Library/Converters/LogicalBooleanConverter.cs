using System.ComponentModel;
using System.Globalization;

namespace Stringify.Converters
{
    /// <summary>
    /// Provides a case-insensitive type converter to convert boolean objects to and from string representations.
    /// 1,t,y,yes are considered as True
    /// 0,f,n,no are considered as False
    /// </summary>
    public class LogicalBooleanConverter : BooleanConverter, ICustomConverter
    {
        /// <inheritdoc />
        public ConverterOptions Options { get; set; }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return base.ConvertFrom(context, culture, null);

            var strVal = value.ToString().ToLower();

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (strVal)
            {
                case "1":
                case "t":
                case "y":
                case "yes":
                    return true;

                case "0":
                case "f":
                case "n":
                case "no":
                    return false;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
