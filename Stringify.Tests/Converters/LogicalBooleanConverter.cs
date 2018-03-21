using System.ComponentModel;
using System.Globalization;
using Stringify.Converters;


namespace Stringify.Tests.Converters
{
    public class FrenchBooleanConverter : LogicalBooleanConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return base.ConvertFrom(context, culture, null);

            var strVal = value.ToString().ToLower();

            if (strVal == "vrai")
                return true;

            if (strVal == "faux")
                return false;

            return base.ConvertFrom(context, culture, value);
        }
    }
}
