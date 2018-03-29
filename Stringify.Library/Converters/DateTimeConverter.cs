using System;
using System.ComponentModel;
using System.Globalization;


namespace Stringify.Converters
{

    /// <summary>
    /// Provides a type converter to convert DateTime objects to and from string representations.
    /// </summary>
    public class DateTimeConverter : System.ComponentModel.DateTimeConverter, ICustomConverter
    {
        /// <inheritdoc />
        public ConverterOptions Options { get; set; }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!string.IsNullOrEmpty(Options.FormatString))
            {
                DateTime result;
                if (DateTime.TryParseExact(value as string, Options.FormatString, culture.DateTimeFormat,
                    Options.DateTimeStyles, out result))
                    return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType != typeof(string) || string.IsNullOrEmpty(Options.FormatString))
                return base.ConvertTo(context, Options.CultureInfo, value, destinationType);

            return ((DateTime) value).ToString(Options.FormatString, Options.CultureInfo.DateTimeFormat);
        }
    }
}
