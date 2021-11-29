using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using BuffettCodeCommon.Period;

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
            json.Add(new JProperty("oldest_date", "2016-11-29T00:00:00.000Z"));
            var (quarterRange, dayRange) = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, quarterRange.From.Year);
            Assert.AreEqual((uint)1, quarterRange.From.Quarter);
            Assert.AreEqual((uint)2020, quarterRange.To.Year);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
            Assert.AreEqual(DayPeriod.Create(DateTime.Today), dayRange.To);
        }
        [TestMethod()]
        public void ParseFloatCase()
        {
            var json = new List<JProperty>();
            json.Add(new JProperty("oldest_fiscal_year", 2016.0));
            json.Add(new JProperty("oldest_fiscal_quarter", 1.0));
            json.Add(new JProperty("latest_fiscal_year", 2020.0));
            json.Add(new JProperty("latest_fiscal_quarter", 4.0));
            json.Add(new JProperty("oldest_date", "2016-11-29T00:00:00.000Z"));
            var (quarterRange, dayRange) = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, quarterRange.From.Year);
            Assert.AreEqual((uint)1, quarterRange.From.Quarter);
            Assert.AreEqual((uint)2020, quarterRange.To.Year);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
        }

        [TestMethod()]
        public void ParseStringCase()
        {
            var json = new List<JProperty>();
            json.Add(new JProperty("oldest_fiscal_year", "2016"));
            json.Add(new JProperty("oldest_fiscal_quarter", "1"));
            json.Add(new JProperty("latest_fiscal_year", "2020"));
            json.Add(new JProperty("latest_fiscal_quarter", "4"));
            var (quarterRange, dayRange) = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, quarterRange.From.Year);
            Assert.AreEqual((uint)1, quarterRange.From.Quarter);
            Assert.AreEqual((uint)2020, quarterRange.To.Year);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
        }

    }
}