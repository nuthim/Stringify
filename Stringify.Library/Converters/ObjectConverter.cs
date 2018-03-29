using System.ComponentModel;
using System.Globalization;

namespace Stringify.Converters
{
    /// <summary>
    /// Provides a type converter to convert objects to and from string representations. 
    /// Complex conversions should be handled by registering a custom converter
    /// </summary>
    public class ObjectConverter : TypeConverter, ICustomConverter
    {
        /// <inheritdoc />
        public ConverterOptions Options { get; set; }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value ?? base.ConvertFrom(context, culture, null);
        }


    }
}
