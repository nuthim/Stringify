Stringify is a type safe string transformation utility. 
It is very common to have the ability to convert string data back and forth to primitive types. Stringify lets to do this and more by extending the idea to any type and providing a overly simple api hiding all complexities. 

#1 Convert to primitive types from string
- Converter.ConvertTo<System.Int32>("5")
- Converter.ConvertTo<int>("$1,000")
- Converter.ConvertTo<int>("  0xCA   ", new ConverterOptions { NumberStyles = NumberStyles.HexNumber})	
- Converter.ConvertTo<DateTime>("28071981", new ConverterOptions { FormatString = "ddMMyyyy"} )
	
#2 Convert to primitive nullable types from string
- Converter.ConvertTo<int?>("5")
	
#3 Convert to Enumerable types from string
- Converter.ConvertTo<IEnumerable<System.Int32>>("1, 2, 3, 4, 5")
- Converter.ConvertTo<IList<System.String>>("1, 2, 3, 4, 5")
- Converter.ConvertTo<List<System.String>>("1, 2, 3, 4, 5")
- Converter.ConvertTo<float[]>("1 2 3 4 5", new ConverterOptions { Delimiter = ' ' })
- Converter.ConvertTo<ICollection<decimal>>("1|    2|  3|4 |   5", new ConverterOptions { Delimiter = '|' })
- Converter.ConvertTo<Collection<short>>("1_2_3_4_5", new ConverterOptions { Delimiter = '_' })

#4 In-built support for converting from logical boolean values
- Converter.ConvertTo<bool>("1") // 1,t,y,yes are considered True irrespective of case
- Converter.ConvertTo<bool>("0") // 0,f,n,no are considered False irrespective of case
- Converter.ConvertTo<IEnumerable<bool>>("true,false")
	
#5 Register your own type conversion and seamlessly convert using the api
TypeConverterFactory.RegisterTypeConverter(typeof(bool), new SomeCustomConverter()); //Handle the conversions as you like
- Converter.ConvertTo<bool>("some value")

#6 All above variants are also available to convert into a string 
- Converter.ConvertFrom(1234.56, new ConverterOptions { FormatString = "#,#.00" })
- Converter.ConvertFrom(new[] { 1, 2, 3, 4, 5 }, new ConverterOptions { Delimiter = '_' }) //returns "1_2_3_4_5"

#7 Opportunities are endless. You can implemenent your own string conversion for specific types and register your type converter with the api and let it take care of the specific conversions as and when required by your client code
