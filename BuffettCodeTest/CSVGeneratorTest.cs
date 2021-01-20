using BuffettCode;
using BuffettCodeAddin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace BuffettCode.UnitTests
{
    [TestClass]
    public class CSVGeneratorTest
    {

        [TestMethod]
        [DeploymentItem(@"data\quarter.json", "data")]
        [DeploymentItem(@"data\csv.txt", "data")]
        public void TestGenerateAndWrite()
        {
            var json = File.ReadAllText(@"data\quarter.json");
            var quarters = Quarter.Parse("2371", json);

            using (var stream = new MemoryStream())
            {
                CSVGenerator.GenerateAndWrite(stream, Encoding.UTF8, quarters);
                var actual = Encoding.UTF8.GetString(stream.ToArray());
                var expected = File.ReadAllText(@"data\csv.txt");
                Assert.AreEqual(actual, expected);
            }
        }

        [TestMethod]
        [DeploymentItem(@"data\quarter.json", "data")]
        [DeploymentItem(@"data\propertyNames.txt", "data")]
        public void TestGetPropertyNames()
        {
            var json = File.ReadAllText(@"data\quarter.json");
            var quarters = Quarter.Parse("2371", json);
            var propertyNames = CSVGenerator.GetPropertyNames(quarters[0]);

            var actual = string.Join(",", propertyNames);
            var expected = File.ReadAllText(@"data\propertyNames.txt");

            Assert.IsTrue(propertyNames.Count == 77);
            Assert.AreEqual(actual, expected);
        }
    }
}