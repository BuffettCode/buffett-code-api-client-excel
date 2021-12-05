using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
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
            var fixedTierRange = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, fixedTierRange.OldestQuarter.Year);
            Assert.AreEqual((uint)1, fixedTierRange.OldestQuarter.Quarter);
            Assert.AreEqual((uint)2020, fixedTierRange.LatestQuarter.Year);
            Assert.AreEqual((uint)4, fixedTierRange.LatestQuarter.Quarter);
            Assert.AreEqual(DayPeriod.Create(oldestDate), fixedTierRange.OldestDate);
            Assert.AreEqual(today, fixedTierRange.LatestDate);
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
            var fixedTierRange = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, fixedTierRange.OldestQuarter.Year);
            Assert.AreEqual((uint)1, fixedTierRange.OldestQuarter.Quarter);
            Assert.AreEqual((uint)2020, fixedTierRange.LatestQuarter.Year);
            Assert.AreEqual((uint)4, fixedTierRange.LatestQuarter.Quarter);
            Assert.AreEqual(DayPeriod.Create(oldestDate), fixedTierRange.OldestDate);
            Assert.AreEqual(today, fixedTierRange.LatestDate);
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
            var fixedTierRange = FixedTierRangeParser.Parse(json);
            Assert.AreEqual((uint)2016, fixedTierRange.OldestQuarter.Year);
            Assert.AreEqual((uint)1, fixedTierRange.OldestQuarter.Quarter);
            Assert.AreEqual((uint)2020, fixedTierRange.LatestQuarter.Year);
            Assert.AreEqual((uint)4, fixedTierRange.LatestQuarter.Quarter);
            Assert.AreEqual(DayPeriod.Create(2016, 11, 30), fixedTierRange.OldestDate);
            Assert.AreEqual(today, fixedTierRange.LatestDate);
        }

    }
}