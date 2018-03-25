using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stringify.Factory;
using Stringify.Tests.Converters;
// ReSharper disable CompareOfFloatsByEqualityOperator
#pragma warning disable 252,253

namespace Stringify.Tests
{
    [TestClass]
    public class ConversionTests
    {
        private static StringConverter Converter
        {
            get; set;
        }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Converter = new StringConverter();
            TypeConverterFactory.RegisterTypeConverter(typeof(bool), new CustomBooleanConverter());
        }

        [TestMethod]
        public void ConvertToPrimitive()
        {
            Assert.IsTrue(Converter.ConvertTo<int>("1000") == 1000);
            Assert.IsTrue(Converter.ConvertTo<uint>("5") == 5u);
            Assert.IsTrue(Converter.ConvertTo<long>("5") == 5);
            Assert.IsTrue(Converter.ConvertTo<ulong>("5") == 5);
            Assert.IsTrue(Converter.ConvertTo<string>("5") == "5");
            Assert.IsTrue(Converter.ConvertTo<string>(null) == null);
            Assert.IsTrue(Converter.ConvertTo<string>(string.Empty) == string.Empty);
            Assert.IsTrue(Converter.ConvertTo<float>("5") == 5.0f);
            Assert.IsTrue(Converter.ConvertTo<double>("5") == 5.0d);
            Assert.IsTrue(Converter.ConvertTo<decimal>("5") == 5.0m);
            Assert.IsTrue(Converter.ConvertTo<short>("5") == 5);
            Assert.IsTrue(Converter.ConvertTo<ushort>("5") == 5);
            Assert.IsTrue(Converter.ConvertTo<byte>("5") == 5);
            Assert.IsTrue(Converter.ConvertTo<sbyte>("5") == 5);
        }

        [TestMethod]
        public void ConvertToPrimitiveWithStyles()
        {
            Assert.IsTrue(Converter.ConvertTo<int>("1,000") == 1000); //Group separator
            Assert.IsTrue(Converter.ConvertTo<int>("-1,000") == -1000); //Sign
            Assert.IsTrue(Converter.ConvertTo<int>("(1,000)") == -1000); //Parantheses indicates negative
            Assert.IsTrue(Converter.ConvertTo<int>("$1,000") == 1000); //Currency
            Assert.IsTrue(Converter.ConvertTo<int>("    1,000   ") == 1000); //White spaces
            Assert.IsTrue(Converter.ConvertTo<int>("  0xCA   ", new ConverterOptions { NumberStyles = NumberStyles.HexNumber}) == 202); //White spaces
            Assert.IsTrue(Converter.ConvertTo<int>("    1,005E2   ") == 100500); //Exponent
            Assert.IsTrue(Converter.ConvertTo<int>("    1,005E02   ") == 100500); //Exponent
            Assert.IsTrue(Converter.ConvertTo<int>("    1,000E-2   ") == 10); //Exponent
            Assert.IsTrue(Converter.ConvertTo<int>("    1,000E-02   ") == 10); //Exponent

            Assert.IsTrue(Converter.ConvertTo<decimal>("1,000") == 1000m); //Group separator
            Assert.IsTrue(Converter.ConvertTo<decimal>("-1,000") == -1000m); //Sign
            Assert.IsTrue(Converter.ConvertTo<decimal>("(1,000)") == -1000m); //Parantheses indicates negative
            Assert.IsTrue(Converter.ConvertTo<decimal>("$1,000") == 1000m); //Currency
            Assert.IsTrue(Converter.ConvertTo<decimal>("    1,000   ") == 1000m); //White spaces
            Assert.IsTrue(Converter.ConvertTo<decimal>("    1,005E-2   ") == 10.05m); //Exponent
        }

        [TestMethod]
        public void ConvertToPrimitiveUnsupportedStyles()
        {
            Assert.IsTrue(Converter.ConvertTo<decimal>("  0xCA   ", new ConverterOptions { NumberStyles = NumberStyles.HexNumber }) == 202m); //Hex not supported on decimal
            Assert.IsTrue(Converter.ConvertTo<float>("  0xCA   ", new ConverterOptions { NumberStyles = NumberStyles.HexNumber }) == 202f); //Hex not supported on float
            Assert.IsTrue(Converter.ConvertTo<double>("  0xCA   ", new ConverterOptions { NumberStyles = NumberStyles.HexNumber }) == 202d); //Hex not supported on double
        }

        [TestMethod]
        public void ConvertNullable()
        {
            Assert.IsTrue(Converter.ConvertTo<int?>("5") == 5);
            Assert.IsTrue(Converter.ConvertTo<int?>(null) == null);
            Assert.IsTrue(Converter.ConvertTo<bool?>(null) == null);
            Assert.IsTrue(Converter.ConvertFrom<int?>(5) == "5");
            Assert.IsTrue(Converter.ConvertFrom<int?>(null) == null);
        }



        [TestMethod]
        public void ConvertFromPrimitive()
        {
            Assert.IsTrue(Converter.ConvertFrom(5) == "5"); //int to string
            Assert.IsTrue(Converter.ConvertFrom(5u) == "5");
            Assert.IsTrue(Converter.ConvertFrom<long>(5) == "5");
            Assert.IsTrue(Converter.ConvertFrom<ulong>(5) == "5");
            Assert.IsTrue(Converter.ConvertFrom("5") == "5");
            Assert.IsTrue(Converter.ConvertFrom<string>(null) == null);
            Assert.IsTrue(Converter.ConvertFrom(string.Empty) == string.Empty);
            Assert.IsTrue(Converter.ConvertFrom(5f) == "5");
            Assert.IsTrue(Converter.ConvertFrom(5.0d) == "5");
            Assert.IsTrue(Converter.ConvertFrom(0.55m) == "0.55");
            Assert.IsTrue(Converter.ConvertFrom<short>(5) == "5");
            Assert.IsTrue(Converter.ConvertFrom<ushort>(5) == "5");
            Assert.IsTrue(Converter.ConvertFrom<byte>(5) == "5");
            Assert.IsTrue(Converter.ConvertFrom<sbyte>(5) == "5");
        }

        [TestMethod]
        public void ConvertObjects()
        {
            Assert.IsTrue(Converter.ConvertTo<object>("5") == "5");
            Assert.IsTrue(Converter.ConvertTo<object>(null) == null);

            Assert.IsTrue(Converter.ConvertFrom<object>("5") == "5");
            Assert.IsTrue(Converter.ConvertFrom<object>(null) == null);
        }

        [TestMethod]
        public void ConvertToBoolean()
        {
            Assert.IsTrue(Converter.ConvertTo<bool>("true"));
            Assert.IsFalse(Converter.ConvertTo<bool>("FALSE"));
            Assert.IsTrue(Converter.ConvertTo<bool>("1"));
            Assert.IsFalse(Converter.ConvertTo<bool>("0"));
            Assert.IsTrue(Converter.ConvertTo<bool>("T"));
            Assert.IsFalse(Converter.ConvertTo<bool>("f"));
            Assert.IsTrue(Converter.ConvertTo<bool>("Y"));
            Assert.IsFalse(Converter.ConvertTo<bool>("n"));
            Assert.IsTrue(Converter.ConvertTo<bool>("Yes"));
            Assert.IsFalse(Converter.ConvertTo<bool>("NO"));
            Assert.IsTrue(Converter.ConvertTo<bool>("VrAi"));
            Assert.IsFalse(Converter.ConvertTo<bool>("FauX"));
        }

        [TestMethod]
        public void ConvertFromBoolean()
        {
            Assert.IsTrue(Converter.ConvertFrom(true) == bool.TrueString);
            Assert.IsTrue(Converter.ConvertFrom(false) == bool.FalseString);
        }

        [TestMethod]
        public void ConvertToIEnumerableBoolean()
        {
            //Alternating true and false values
            var array = Converter.ConvertTo<IEnumerable<bool>>("true,false,TRUE,FALSE,T,F,t,f,1,0,yes,no,YES,NO,Y,N,VRAI,faux").ToArray();

            //Assert all even indexed elements are true
            var trueArray = array.Where((x, i) => i % 2 == 0);
            Assert.IsTrue(trueArray.Aggregate(true, (x, result) => x & result));

            //Assert all odd indexed elements are false
            var falseArray = array.Where((x, i) => i % 2 != 0);
            Assert.IsFalse(falseArray.Any(x => x));
        }

        [TestMethod]
        public void ConvertToArray()
        {
            Assert.IsTrue(Converter.ConvertTo<string[]>("1; 2; 3; 4; 5", new ConverterOptions { Delimiter = ';'}).Length == 5);
            Assert.IsTrue(Converter.ConvertTo<int[]>("1, 2, 3, 4, 5").Length == 5);
            Assert.IsTrue(Converter.ConvertTo<float[]>("1 2 3 4 5", new ConverterOptions { Delimiter = ' ' }).Length == 5);
            Assert.IsTrue(Converter.ConvertTo<decimal[]>("1|    2|  3|4 |   5", new ConverterOptions { Delimiter = '|' }).Length == 5);
            Assert.IsTrue(Converter.ConvertTo<uint[]>("1\t2\t3\t4\t5", new ConverterOptions { Delimiter = '\t' }).Length == 5);
            Assert.IsTrue(Converter.ConvertTo<short[]>("1_2_3_4_5", new ConverterOptions { Delimiter = '_' }).Length == 5);
        }

        [TestMethod]
        public void ConvertFromArray()
        {
            Assert.IsTrue(Converter.ConvertFrom(new[] {"1", null, string.Empty, "!@#$%^&*()", "happy mother's day!" }) == "1,,,!@#$%^&*(),happy mother's day!");
            Assert.IsTrue(Converter.ConvertFrom(new[] { 1, 2, 3, 4, 5 }, new ConverterOptions { Delimiter = '_' }) == "1_2_3_4_5");
            Assert.IsTrue(Converter.ConvertFrom(new[] { 0, 2.5m, 3, 445.45m, 5 }) == "0,2.5,3,445.45,5");
            Assert.IsTrue(Converter.ConvertFrom(new ushort[] { 1, 2, 3, 4, 5 }, new ConverterOptions { Delimiter = '\t' }) == "1\t2\t3\t4\t5");
        }

        [TestMethod]
        public void ConvertToEnumerable()
        {
            const string array = "1, 2, 3, 4, 5";
            Assert.IsTrue(Converter.ConvertTo<IEnumerable<string>>(array).Count() == 5);
            Assert.IsTrue(Converter.ConvertTo<IEnumerable<int>>(array).Count() == 5);
            Assert.IsTrue(Converter.ConvertTo<IEnumerable<float>>(array).Count() == 5);
            Assert.IsTrue(Converter.ConvertTo<IEnumerable<decimal>>(array).Count() == 5);
            Assert.IsTrue(Converter.ConvertTo<IEnumerable<uint>>(array).Count() == 5);
            Assert.IsTrue(Converter.ConvertTo<IEnumerable<short>>(array).Count() == 5);
        }

        [TestMethod]
        public void ConvertFromEnumerable()
        {
            Assert.IsTrue(Converter.ConvertFrom<IEnumerable<string>>(new[] { "1", "2", "3", "4", "5" }) == "1,2,3,4,5");
            Assert.IsTrue(Converter.ConvertFrom<IEnumerable<int>>(new[] { 1, 2, 3, 4, 5 }) == "1,2,3,4,5");
            Assert.IsTrue(Converter.ConvertFrom<IEnumerable<decimal>>(new decimal[] { 1, 2, 3, 4, 5 }) == "1,2,3,4,5");
            Assert.IsTrue(Converter.ConvertFrom<IEnumerable<ushort>>(new ushort[] { 1, 2, 3, 4, 5 }) == "1,2,3,4,5");
        }

        [TestMethod]
        public void ConvertToIList()
        {
            string array = "1, 2, 3, 4, 5";
            Assert.IsTrue(Converter.ConvertTo<IList<string>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<IList<int>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<IList<float>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<IList<decimal>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<IList<uint>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<IList<short>>(array).Count == 5);
        }

        [TestMethod]
        public void ConvertToList()
        {
            string array = "1, 2, 3, 4, 5";
            Assert.IsTrue(Converter.ConvertTo<List<string>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<List<int>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<List<float>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<List<decimal>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<List<uint>>(array).Count == 5);
            Assert.IsTrue(Converter.ConvertTo<List<short>>(array).Count == 5);
        }

        [TestMethod]
        public void ConvertFromComplexType()
        {
            var employee = new Employee { Id = 1, Name = "Mithun" };
            Assert.IsTrue(Converter.ConvertFrom(employee) == employee.ToString());
        }

    }

    internal class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }

   
}
