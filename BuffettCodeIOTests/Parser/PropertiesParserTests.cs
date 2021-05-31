using BuffettCodeAPIClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class PropertiesParserTests
    {
        [TestMethod()]
        [DeploymentItem(@"TestData\Properties.json", @"TestData")]
        public void ParseTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\Properties.json")).Cast<JProperty>();
            var properties = PropertiesParser.Parse(json);
            Assert.AreEqual(properties.Get("company_name"), "株式会社カカクコム");
            Assert.AreEqual(properties.Get("current_assets"), "28774000000");
        }
    }
}