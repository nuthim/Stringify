using System;
using System.ComponentModel;
using System.Globalization;


namespace Stringify.Converters
{
    /// <summary>
    /// Base converter for numeric types
    /// </summary>
    public abstract class BaseNumberConverter : TypeConverter, ICustomConverter
    {
        /// <inheritdoc />
        public ConverterOptions Options { get; set; }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var s = value as string;
            if (s == null)
                return base.ConvertFrom(context, Options.CultureInfo, value);

            s = s.Trim();
            if ((Options.NumberStyles & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
                s = s.Replace("0x", string.Empty).Replace("&h", string.Empty).Replace("0X", string.Empty).Replace("&H", string.Empty);

            return FromString(s.Trim());
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType != typeof(string) || string.IsNullOrEmpty(Options.FormatString))
                return base.ConvertTo(context, Options.CultureInfo, value, destinationType);

            return ToString(value, Options.FormatString);
        }

        internal abstract object FromString(string value);

        internal abstract string ToString(object value, string format);
    }
}
