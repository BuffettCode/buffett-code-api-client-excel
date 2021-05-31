using BuffettCodeAPIClient;
using BuffettCodeIO.Parser;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BuffettCodeAddinRibbon.UnitTests
{
    [TestClass]
    public class CSVGeneratorTest
    {

        private static IList<Quarter> ReadV2Quarter()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"data\quarter.json"));
            return ApiV2ResponseParser.ParseQuarterRange(json);
        }

        [TestMethod]
        [DeploymentItem(@"data\quarter.json", "data")]
        [DeploymentItem(@"data\csv.txt", "data")]
        public void TestGenerateAndWrite()
        {
            var quarters = ReadV2Quarter();

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
            var quarters = ReadV2Quarter();
            var propertyNames = CSVGenerator.GetPropertyNames(quarters[0]);

            var actual = string.Join(",", propertyNames);
            var expected = File.ReadAllText(@"data\propertyNames.txt");

            Assert.IsTrue(propertyNames.Count == 77);
            Assert.AreEqual(actual, expected);
        }
    }
}