using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Stringify.Tests
{
    [TestClass]
    public class JsonConversionTests
    {
        private static StringConverter Converter
        {
            get; set;
        }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            Converter = new StringConverter();
        }

        [TestMethod]
        public void ConvertToJsonStudent()
        {
            var student = new Student { Id = 101, Name = "Mithun Basak" };
            string json = JsonConvert.SerializeObject(student, Formatting.None);
            var x = Converter.ConvertTo<Student>(json, new ConverterOptions { StringFormat = Format.Json });
            Assert.IsTrue(x.Name == student.Name && x.Id == 101);
        }

        [TestMethod]
        public void ConvertToJsonStudentsArray()
        {
            var students = new[]
            {
                new Student { Id = 101, Name = "Mithun Basak" },
                new Student { Id = 102, Name = "Deepa Basak" }
            };
            string json = JsonConvert.SerializeObject(students, Formatting.None);
            var x = Converter.ConvertTo<Student[]>(json, new ConverterOptions { StringFormat = Format.Json });
            Assert.IsTrue(x.Count() == 2);
        }

        [TestMethod]
        public void ConvertToJsonStudentsList()
        {
            var students = new[]
            {
                new Student { Id = 101, Name = "Mithun Basak" },
                new Student { Id = 102, Name = "Deepa Basak" }
            };
            string json = JsonConvert.SerializeObject(students, Formatting.None);
            var x = Converter.ConvertTo<List<Student>>(json, new ConverterOptions { StringFormat = Format.Json });
            Assert.IsTrue(x.Count() == 2);
        }


        [TestMethod]
        public void ConvertToJsonStudentsIEnumerable()
        {
            var students = new[]
            {
                new Student { Id = 101, Name = "Mithun Basak" },
                new Student { Id = 102, Name = "Deepa Basak" }
            };
            string json = JsonConvert.SerializeObject(students, Formatting.None);
            var x = Converter.ConvertTo<IEnumerable<Student>>(json, new ConverterOptions { StringFormat = Format.Json });
            Assert.IsTrue(x.Count() == 2);
        }


        [TestMethod]
        public void ConvertToJsonStudentsIList()
        {
            var students = new[]
            {
                new Student { Id = 101, Name = "Mithun Basak" },
                new Student { Id = 102, Name = "Deepa Basak" }
            };
            string json = JsonConvert.SerializeObject(students, Formatting.None);
            var x = Converter.ConvertTo<IList<Student>>(json, new ConverterOptions { StringFormat = Format.Json });
            Assert.IsTrue(x.Count() == 2);
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
