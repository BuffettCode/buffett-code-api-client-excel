using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class FixedTierRangeParserTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            var json = new List<JProperty>();
            json.Add(new JProperty("oldest_fiscal_year", 2016));
            json.Add(new JProperty("oldest_fiscal_quarter", 1));
            json.Add(new JProperty("latest_fiscal_year", 2020));
            json.Add(new JProperty("latest_fiscal_quarter", 4));
            var fixedTierRange = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, fixedTierRange.From.Year);
            Assert.AreEqual((uint)1, fixedTierRange.From.Quarter);
            Assert.AreEqual((uint)2020, fixedTierRange.To.Year);
            Assert.AreEqual((uint)4, fixedTierRange.To.Quarter);
        }
    }
}