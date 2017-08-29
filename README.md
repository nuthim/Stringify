Stringify is a type safe string transformation utility. 
It is very common to have the ability to convert string data back and forth to primitive types. Stringify lets to do this and more by extending the idea to any type and providing a overly simple api hiding all complexities. 

#1 Convert to primitive types
- Converter.ConvertTo<System.Int32>("5")
	
#2 Convert to primitive types (nullable)
- Converter.ConvertTo<int?>("5")
	
#3 Pre-Register your own type conversion and seamlessly convert using the same api
- Converter.ConvertTo<System.Boolean>("T")
	
#4 Convert to Enumerable types 
- Converter.ConvertTo<IEnumerable<System.Int32>>("1, 2, 3, 4, 5")
- Converter.ConvertTo<IList<System.String>>("1, 2, 3, 4, 5")
- Converter.ConvertTo<List<System.String>>("1, 2, 3, 4, 5")

#5 Convert to complex types from json string representation
- Converter.ConvertTo<Namespace.Student>("{\"Id\":101, \"Name\":\"Mithun Basak\"}")

#6 Opportunities are endless. You can implemenent your own string conversion for specific types and register your type converter with the api and let it take care of the specific conversions as and when required by your client code
