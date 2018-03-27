using System.Globalization;


namespace Stringify
{
    public class ConverterOptions
    {
        public static readonly ConverterOptions _defaultOptions;
        private const char DefaultDelimiter = ',';
        private char? _customDelimiter;
        private CultureInfo _cultureInfo;
        private NumberStyles? _styles;
        static ConverterOptions()
        {
            _defaultOptions = new ConverterOptions
            {
                Delimiter = DefaultDelimiter
            };
        }

        public static ConverterOptions Default => _defaultOptions;

        /// <summary>
        /// Applicable only for a collection of items. During the conversion process the delimiter is used to split the items
        /// </summary>
        public char Delimiter
        {
            get { return _customDelimiter ?? DefaultDelimiter; }
            set { _customDelimiter = value; }
        }

        /// <summary>
        /// Controls if the final output is trimmed off any white space characters. Default is False.
        /// </summary>
        public bool TrimElements { get; set; }

        public CultureInfo CultureInfo
        {
            get { return _cultureInfo ?? CultureInfo.CurrentCulture; }
            set { _cultureInfo = value; }
        }

        public NumberStyles NumberStyles
        {
            get { return _styles ?? NumberStyles.Any; }
            set { _styles = value; }
        }
    }
}
