using System.ComponentModel;
using System.Globalization;

namespace Stringify.Converters
{
    public class LogicalBooleanConverter : BooleanConverter, ICustomConverter
    {
        public ConverterOptions Options { get; set; }

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
