using Newtonsoft.Json;
using System.Xml;

namespace Stringify
{
    public class ConverterOptions
    {
        public static readonly ConverterOptions _defaultOptions;
        private const char DefaultDelimiter = ',';
        private char? _customDelimiter;

        static ConverterOptions()
        {
            _defaultOptions = new ConverterOptions
            {
                Delimiter = DefaultDelimiter,
                StringFormat = Format.None
            };
        }

        public static ConverterOptions Default
        {
            get { return _defaultOptions; }
        }

        public char Delimiter
        {
            get { return _customDelimiter ?? DefaultDelimiter; }
            set { _customDelimiter = value; }
        }

        public Format StringFormat
        {
            get; set;
        }

        public JsonSerializerSettings JsonSettings { get; set; }

        public XmlReaderSettings XmlSettings { get; set; }
    }

    public enum Format
    {
        None = 0,
        Json = 1,
        Xml
    }
}
