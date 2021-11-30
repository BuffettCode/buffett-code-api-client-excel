using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System;
using System.Collections.Generic;

namespace BuffettCodeIO.Parser.Tests
{
    [TestClass()]
    public class FixedTierRangeParserTests
    {
        private static readonly DayPeriod today = DayPeriod.Create(DateTime.Today);
        [TestMethod()]

        public void ParseTest()
        {
            var oldestDate = DateTime.Today.AddDays(-365 * 5);
            var json = new List<JProperty>();
            json.Add(new JProperty("oldest_fiscal_year", 2016));
            json.Add(new JProperty("oldest_fiscal_quarter", 1));
            json.Add(new JProperty("latest_fiscal_year", 2020));
            json.Add(new JProperty("latest_fiscal_quarter", 4));
            json.Add(new JProperty("oldest_date", oldestDate));
            var (quarterRange, dayRange) = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, quarterRange.From.Year);
            Assert.AreEqual((uint)1, quarterRange.From.Quarter);
            Assert.AreEqual((uint)2020, quarterRange.To.Year);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
            Assert.AreEqual(DayPeriod.Create(oldestDate), dayRange.From);
            Assert.AreEqual(today, dayRange.To);
        }
        [TestMethod()]
        public void ParseFloatCase()
        {
            var json = new List<JProperty>();
            var oldestDate = DateTime.Today.AddDays(-365 * 5);
            json.Add(new JProperty("oldest_fiscal_year", 2016.0));
            json.Add(new JProperty("oldest_fiscal_quarter", 1.0));
            json.Add(new JProperty("latest_fiscal_year", 2020.0));
            json.Add(new JProperty("latest_fiscal_quarter", 4.0));
            json.Add(new JProperty("oldest_date", oldestDate.ToString()));
            var (quarterRange, dayRange) = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, quarterRange.From.Year);
            Assert.AreEqual((uint)1, quarterRange.From.Quarter);
            Assert.AreEqual((uint)2020, quarterRange.To.Year);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
            Assert.AreEqual(DayPeriod.Create(oldestDate), dayRange.From);
            Assert.AreEqual(today, dayRange.To);
        }

        [TestMethod()]
        public void ParseStringCase()
        {
            var json = new List<JProperty>();
            json.Add(new JProperty("oldest_fiscal_year", "2016"));
            json.Add(new JProperty("oldest_fiscal_quarter", "1"));
            json.Add(new JProperty("latest_fiscal_year", "2020"));
            json.Add(new JProperty("latest_fiscal_quarter", "4"));
            json.Add(new JProperty("oldest_date", "2016-11-30"));
            var (quarterRange, dayRange) = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, quarterRange.From.Year);
            Assert.AreEqual((uint)1, quarterRange.From.Quarter);
            Assert.AreEqual((uint)2020, quarterRange.To.Year);
            Assert.AreEqual((uint)4, quarterRange.To.Quarter);
            Assert.AreEqual(DayPeriod.Create(2016, 11, 30), dayRange.From);
            Assert.AreEqual(DayPeriod.Create(DateTime.Today), dayRange.To);
        }

    }
}