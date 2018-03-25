using System.ComponentModel;
using System.Globalization;


namespace Stringify.Converters
{
    public abstract class BaseNumberConverter : TypeConverter
    {
        public ConverterOptions Options { get; set; }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var s = value as string;
            if (s == null)
                return base.ConvertFrom(context, culture, value);

            var formatInfo = culture.GetFormat(typeof(NumberFormatInfo));
            s = s.Trim();
            if ((Options.NumberStyles & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
                s = s.Replace("0x", string.Empty).Replace("&h", string.Empty).Replace("0X", string.Empty).Replace("&H", string.Empty);

            return FromString(s.Trim(), (NumberFormatInfo)formatInfo);
        }

        internal abstract object FromString(string value, NumberFormatInfo formatInfo);
    }
}
