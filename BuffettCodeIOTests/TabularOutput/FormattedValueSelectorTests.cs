using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeIO.TabluarOutput.Tests
{
    [TestClass()]
    public class FormattedValueSelectorTests
    {
        private static readonly string ticker = "1234";
        private static readonly string key = "test_key";
        private static readonly string value = "100000000000";
        private static readonly string label = "ラベル";
        private static readonly string unit = "百万円";

        private static readonly FiscalQuarterPeriod period = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly IDictionary<string, string> properties = new Dictionary<string, string> { { key, value } };
        private static readonly IDictionary<string, PropertyDescription> descriptions = new Dictionary<string, PropertyDescription> { { key, new PropertyDescription(key, label, unit) } };

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
        public void SelectQuarterTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            var selector = new FormattedValueSelector(quarter);
            Assert.AreEqual("100,000", selector.Select(key));
        }


    }
}