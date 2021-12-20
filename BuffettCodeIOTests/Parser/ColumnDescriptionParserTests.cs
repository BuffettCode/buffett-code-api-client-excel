using BuffettCodeAPIClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class ColumnDescriptionParserTests
    {
        [TestMethod()]
        [DeploymentItem(@"TestData\ColumnDescription.json", @"TestData")]
        public void ParseTest()
        {
            var json = ApiGetResponseBodyParser.Parse(File.ReadAllText(@"TestData\ColumnDescription.json"));
            var descriptions = ColumnDescriptionParser.Parse(json);
            var dividend = descriptions.Get("dividend");
            Assert.AreEqual(dividend.Name, "dividend");
            Assert.AreEqual(dividend.JpName, "配当金");
            Assert.AreEqual(dividend.Unit, "円");

            var ordinary_income = descriptions.Get("ordinary_income");
            Assert.AreEqual(ordinary_income.Name, "ordinary_income");
            Assert.AreEqual(ordinary_income.JpName, "経常利益");
            Assert.AreEqual(ordinary_income.Unit, "百万円");
        }
    }
}