using System.ComponentModel;
using System.Globalization;

namespace Stringify.Converters
{
    public class ObjectConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value ?? base.ConvertFrom(context, culture, null);
        }
    }
}
