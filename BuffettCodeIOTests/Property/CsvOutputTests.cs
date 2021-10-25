using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Property.Tests
{
    [TestClass()]
    public class CsvOutputTests
    {
        private static readonly string ticker = "1234";
        private static readonly string key = "test_key";
        private static readonly string value = "test_value";
        private static readonly string label = "ラベル";
        private static readonly string unit = "test_unit";

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
        public void QuarterTest()
        {
            var quarter = CreateQuarter(ticker, period, properties, descriptions);
            var csvOutput = new CsvOutput<Quarter>();
            var rows = csvOutput.Add(quarter).ToRows().ToArray();
            var header = rows[0];
            var firstDataRow = rows[1];
            Assert.AreEqual("キー", header.Key);
            Assert.AreEqual("項目名", header.Name);
            Assert.AreEqual("単位", header.Unit);
            Assert.AreEqual(1, header.Values.Count);
            Assert.AreEqual(period.ToString(), header.Values[0]);

            Assert.AreEqual(key, firstDataRow.Key);
            Assert.AreEqual(label, firstDataRow.Name);
            Assert.AreEqual(unit, firstDataRow.Unit);
            Assert.AreEqual(1, firstDataRow.Values.Count);
            Assert.AreEqual(value, firstDataRow.Values[0]);
        }
    }
}