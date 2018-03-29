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
        private DateTimeStyles? _dtStyles;

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
        /// When converting to string, specifies the format of the output string. If a collection of items is being converted then each
        /// item will be output in the specified format.
        /// Also applicable when converting to <see cref="System.DateTime"/> to provision specifying the exact format 
        /// </summary>
        public string FormatString { get; set; }

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

        public DateTimeStyles DateTimeStyles
        {
            get { return _dtStyles ?? DateTimeStyles.None; }
            set { _dtStyles = value; }
        }
    }
}
