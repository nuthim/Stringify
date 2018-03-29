

namespace Stringify.Converters
{
    /// <summary>
    /// Marker for a custom converter which requires <see cref="ConverterOptions"/> 
    /// </summary>
    public interface ICustomConverter
    {
        /// <summary>
        /// Options to customize the conversion
        /// </summary>
        ConverterOptions Options { get; set; }
    }
}
