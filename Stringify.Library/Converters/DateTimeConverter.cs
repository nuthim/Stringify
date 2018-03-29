using System;
using System.ComponentModel;
using System.Globalization;


namespace Stringify.Converters
{
    public class DateTimeConverter : System.ComponentModel.DateTimeConverter, ICustomConverter
    {
        public ConverterOptions Options { get; set; }

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

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType != typeof(string) || string.IsNullOrEmpty(Options.FormatString))
                return base.ConvertTo(context, Options.CultureInfo, value, destinationType);

            return ((DateTime) value).ToString(Options.FormatString, Options.CultureInfo.DateTimeFormat);
        }
    }
}
