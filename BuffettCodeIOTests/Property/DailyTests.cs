using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;


namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class DailyTests
    {
        private static readonly string ticker = "1234";
        private static readonly DayPeriod period = DayPeriod.Create(2020, 1, 1);
        private static readonly IDictionary<string, string> properties = new Dictionary<string, string> { { "key", "value" } };
        private static readonly IDictionary<string, PropertyDescription> descriptions = new Dictionary<string, PropertyDescription> { { "key", new PropertyDescription("name", "label", "unit") } };

        private static Daily CreateDaily(string ticker, DayPeriod period, IDictionary<string, string> properties, IDictionary<string, PropertyDescription> descriptions)
        {
            return Daily.Create(
                ticker,
                period,
                properties is null ? PropertyDictionary.Empty() : new PropertyDictionary(properties),
                descriptions is null ? PropertyDescriptionDictionary.Empty() : new PropertyDescriptionDictionary(descriptions)
            );
        }



        [TestMethod()]
        public void GetDescriptionTest()
        {
            var daily = CreateDaily(ticker, period, properties, descriptions);
            Assert.AreEqual("name", daily.GetDescription("key").Name);
            Assert.AreEqual("label", daily.GetDescription("key").JpName);
            Assert.AreEqual("unit", daily.GetDescription("key").Unit);
        }

        [TestMethod()]
        public void GetPeriodTest()
        {
            var daily = CreateDaily(ticker, period, properties, descriptions);
            Assert.AreEqual(period, daily.Period);
        }

        [TestMethod()]
        public void GetPropertyNamesTest()
        {
            var daily = CreateDaily(ticker, period, properties, descriptions);
            Assert.AreEqual("key", daily.GetPropertyNames().First());
            Assert.AreEqual(1, daily.GetPropertyNames().Count());
        }

        [TestMethod()]
        public void GetValueTest()
        {
            var daily = CreateDaily(ticker, period, properties, descriptions);
            Assert.AreEqual("value", daily.GetValue("key"));
        }

        [TestMethod()]
        public void CreateTest()
        {
            var daily = Daily.Create(
                ticker,
                period,
                new PropertyDictionary(properties),
                new PropertyDescriptionDictionary(descriptions)
            );
            Assert.AreEqual(ticker, daily.Ticker);

            Assert.ThrowsException<ValidationError>(() => Daily.Create(
                "dummy",
                period,
                new PropertyDictionary(properties),
                new PropertyDescriptionDictionary(descriptions)
            ));
        }
    }
}