using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuffettCodeExcelFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;

namespace BuffettCodeExcelFunctions.Tests
{
    [TestClass()]
    public class PropertySelectorTests
    {
        private static readonly string ticker = "1234";
        private static readonly FiscalQuarterPeriod period = FiscalQuarterPeriod.Create(2021, 3);
        private static readonly IDictionary<string, string> properties = new Dictionary<string, string> { { "ceo_name", "Mr. CEO" }, {"net_sales", "1000000000" } };
        private static readonly IDictionary<string, PropertyDescription> descriptions = new Dictionary<string, PropertyDescription> { { "ceo_name", new PropertyDescription("ceo_name", "代表者名", "なし") }, { "net_sales", new PropertyDescription("net_sales", "売上", "円") } };


        [TestMethod()]
        public void SelectFormattedValueTest()
        {
            var quarter = Quarter.Create(
                    ticker,
                    period,
                    new PropertyDictionary(properties),
                    new PropertyDescriptionDictionary(descriptions)
                );

            // select ceo_name
            Assert.AreEqual("Mr. CEO", PropertySelector.SelectFormattedValue("ceo_name", quarter, false, false, false));
            Assert.AreEqual("Mr. CEO", PropertySelector.SelectFormattedValue("ceo_name", quarter, true, false, false));
            Assert.AreEqual("Mr. CEO", PropertySelector.SelectFormattedValue("ceo_name", quarter, false, true, false));
            Assert.AreEqual("Mr. CEO", PropertySelector.SelectFormattedValue("ceo_name", quarter, false, false, true));

            // select net_sales
            Assert.AreEqual("1,000,000,000", PropertySelector.SelectFormattedValue("net_sales", quarter, false, false, false));
            Assert.AreEqual("1000000000", PropertySelector.SelectFormattedValue("net_sales", quarter, true, false, false));
            Assert.AreEqual("1,000", PropertySelector.SelectFormattedValue("net_sales", quarter, false, false, true));
            Assert.AreEqual("1,000,000,000円", PropertySelector.SelectFormattedValue("net_sales", quarter, false, true, false));
            Assert.AreEqual("1000000000円", PropertySelector.SelectFormattedValue("net_sales", quarter, true, true, false));
            Assert.AreEqual("1,000百万円", PropertySelector.SelectFormattedValue("net_sales", quarter, false, true, true));
        }


    }
}