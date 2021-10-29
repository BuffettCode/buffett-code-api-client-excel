using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.TabluarOutput.Tests
{
    [TestClass()]
    public class TabluarFormatterTests
    {
        private static readonly string ticker = "1234";
        private static readonly string key = "test_key";
        private static readonly string value = "test_value1";
        private static readonly string label = "ラベル";
        private static readonly string unit = "test_unit";

        private static readonly FiscalQuarterPeriod period2021Q3 = FiscalQuarterPeriod.Create(2021, 3);

        private static readonly FiscalQuarterPeriod period2021Q4 = FiscalQuarterPeriod.Create(2021, 4);

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
        public void FormatQuarterTest()
        {
            var quarter1 = CreateQuarter(ticker, period2021Q3, properties, descriptions);
            var quarter2 = CreateQuarter(ticker, period2021Q4, properties, descriptions);
            var csvOutput = TabularFormatter<Quarter>.Format(new List<Quarter> { quarter1, quarter2 });
            var rows = csvOutput.ToRows().ToArray();
            Assert.AreEqual(2, rows.Length);

            var header = rows[0];
            var firstDataRow = rows[1];
            // check header
            Assert.AreEqual("キー", header.Key);
            Assert.AreEqual("項目名", header.Name);
            Assert.AreEqual("単位", header.Unit);
            Assert.AreEqual(2, header.Values.Count);
            Assert.AreEqual(period2021Q3.ToString(), header.Values[0]);
            Assert.AreEqual(period2021Q4.ToString(), header.Values[1]);

            // check "key" row
            Assert.AreEqual(key, firstDataRow.Key);
            Assert.AreEqual(label, firstDataRow.Name);
            Assert.AreEqual(unit, firstDataRow.Unit);
            Assert.AreEqual(2, firstDataRow.Values.Count);
            Assert.AreEqual(value, firstDataRow.Values[0]);
            Assert.AreEqual(value, firstDataRow.Values[1]);
        }
    }
}