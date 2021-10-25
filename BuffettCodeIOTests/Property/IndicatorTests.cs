using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class IndicatorTests
    {

        private static readonly string ticker = "1234";
        private static readonly IDictionary<string, string> properties = new Dictionary<string, string> { { "key", "value" } };
        private static readonly IDictionary<string, PropertyDescription> descriptions = new Dictionary<string, PropertyDescription> { { "key", new PropertyDescription("name", "label", "unit") } };

        private static Indicator CreateIndicator(IDictionary<string, string> properties, IDictionary<string, PropertyDescription> descriptions)
        {
            return Indicator.Create(
                ticker,
                properties is null ? PropertyDictionary.Empty() : new PropertyDictionary(properties),
                descriptions is null ? PropertyDescriptionDictionary.Empty() : new PropertyDescriptionDictionary(descriptions)
            );
        }


        [TestMethod()]
        public void GetValueTest()
        {
            var indicator = CreateIndicator(properties, descriptions);
            Assert.AreEqual("value", indicator.GetValue("key"));
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            var indicator = CreateIndicator(properties, descriptions);
            Assert.AreEqual("name", indicator.GetDescription("key").Name);
            Assert.AreEqual("label", indicator.GetDescription("key").Label);
            Assert.AreEqual("unit", indicator.GetDescription("key").Unit);
        }

        [TestMethod()]
        public void GetPeriodTest()
        {
            var indicator = CreateIndicator(properties, descriptions);
            Assert.AreEqual(Snapshot.GetInstance(), indicator.GetPeriod());
        }



    }
}