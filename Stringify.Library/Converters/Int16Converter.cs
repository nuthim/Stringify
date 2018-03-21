﻿using System.Globalization;

namespace Stringify.Converters
{
    public class Int16Converter : BaseNumberConverter
    {
        internal override object FromString(string value, NumberFormatInfo formatInfo)
        {
            return short.Parse(value, Options.NumberStyles, formatInfo);
        }
    }
}