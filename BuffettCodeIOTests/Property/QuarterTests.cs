using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class QuarterTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod period = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly IDictionary<string, string> properties = new Dictionary<string, string> { { "key", "value" } };
        private static readonly IDictionary<string, PropertyDescription> descriptions = new Dictionary<string, PropertyDescription> { { "key", new PropertyDescription("name", "label", "unit") } };

        private static Quarter CreateQuarter(string ticker, FiscalQuarterPeriod period, IDictionary<string, string> properties, IDictionary<string, PropertyDescription> descriptions)
        {
            return Quarter.Create(
                ticker,
                period,
                properties is null ? PropertyDictionary.Empty() : new PropertyDictionary(properties),
                descriptions is null ? PropertyDescriptionDictionary.Empty() : new PropertyDescriptionDictionary(descriptions)
            );

        }


        [TestMethod()]
        public void PeriodTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            Assert.AreEqual(period, quarter.Period);
        }

        [TestMethod()]
        public void GetPropartyNamesTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            Assert.AreEqual("key", quarter.GetPropartyNames().First());
            Assert.AreEqual(1, quarter.GetPropartyNames().Count());
        }

        [TestMethod()]
        public void GetValueTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            Assert.AreEqual("value", quarter.GetValue("key"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            Assert.AreEqual("name", quarter.GetDescription("key").Name);
            Assert.AreEqual("label", quarter.GetDescription("key").Label);
            Assert.AreEqual("unit", quarter.GetDescription("key").Unit);
        }


        [TestMethod()]
        public void EqualsTest()
        {
            var a = CreateQuarter(ticker, period, properties, descriptions);
            var b = CreateQuarter(ticker, period, properties, descriptions);
            var c = CreateQuarter(ticker, period, null, null);
            var d = CreateQuarter("2345", period, properties, descriptions);
            var e = CreateQuarter("2345", FiscalQuarterPeriod.Create(2000, 1), properties, descriptions);
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            var a = CreateQuarter(ticker, period, properties, descriptions);
            var b = CreateQuarter(ticker, period, properties, descriptions);
            var c = CreateQuarter(ticker, period, null, null);
            var d = CreateQuarter("2345", period, properties, descriptions);
            var e = CreateQuarter(ticker, FiscalQuarterPeriod.Create(2000, 1), properties, descriptions);
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreNotEqual(a, e);
        }

        [TestMethod()]
        public void DistenctTest()
        {
            var a = CreateQuarter(ticker, period, properties, descriptions);
            var b = CreateQuarter(ticker, period, properties, descriptions);
            var c = CreateQuarter(ticker, period, null, null);
            var list = new List<Quarter> { a, b, c };
            Assert.AreEqual(3, list.Count());
            Assert.AreEqual(2, list.Distinct().Count());
        }

        [TestMethod()]
        public void SortByPeriod()
        {

            var a = CreateQuarter(ticker, FiscalQuarterPeriod.Create(2021, 3), null, null);
            var b = CreateQuarter(ticker, FiscalQuarterPeriod.Create(2021, 4), null, null);
            var c = CreateQuarter(ticker, FiscalQuarterPeriod.Create(2018, 1), null, null);

            var list = new List<Quarter> { a, b, c };
            var sorted = list.OrderBy(q => q.Period).ToList();
            Assert.AreEqual(FiscalQuarterPeriod.Create(2018, 1), sorted[0].Period);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2021, 3), sorted[1].Period);
            Assert.AreEqual(FiscalQuarterPeriod.Create(2021, 4), sorted[2].Period);
        }
    }


}