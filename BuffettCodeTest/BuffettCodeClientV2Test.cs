using BuffettCodeAddin;
using BuffettCodeAddin.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Buffett
{
    [TestClass]
    public class BuffettCodeClientV2Test
    {
        [TestMethod]
        public void TestGetQuarter()
        {
            var client = new BuffettCodeClientV2();
            var json = client.GetQuarter(BuffettCodeTestUtils.GetValidApiKey(), "6501", "2018", "4").Result;
            var quarters = Quarter.Parse("6501", json);

            Assert.IsFalse(quarters.Count == 0);
            Assert.AreEqual("6501", quarters[0].Ticker);
            Assert.IsNotNull(quarters[0].GetValue("assets")); // 値は変わりうるのでNullかどうかだけチェック
            Assert.IsNotNull(quarters[0].GetDescription("assets"));
        }

        [TestMethod]
        public void TestGetQuarterRange()
        {
            var client = new BuffettCodeClientV2();
            var json = client.GetQuarterRange(BuffettCodeTestUtils.GetValidApiKey(), "6501", "2013Q1", "2015Q4").Result;
            var quarters = Quarter.Parse("6501", json);

            Assert.IsFalse(quarters.Count == 0);
            Assert.AreEqual("6501", quarters[0].Ticker);
            Assert.IsNotNull(quarters[0].GetValue("assets")); // 値は変わりうるのでNullかどうかだけチェック
            Assert.IsNotNull(quarters[0].GetDescription("assets"));
        }

        [TestMethod]
        public void TestGetIndicator()
        {
            var client = new BuffettCodeClientV2();
            var json = client.GetIndicator(BuffettCodeTestUtils.GetValidApiKey(), "6501").Result;
            var indicators = Indicator.Parse("6501", json);

            Assert.IsFalse(indicators.Count == 0);
            Assert.AreEqual("6501", indicators[0].Ticker);
            Assert.IsNotNull(indicators[0].GetValue("roe")); // 値は変わりうるのでNullかどうかだけチェック
            Assert.IsNotNull(indicators[0].GetDescription("roe"));
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestInvalidApiKey()
        {
            var client = new BuffettCodeClientV2();
            var json = client.GetQuarter(BuffettCodeTestUtils.GetInvalidApiKey(), "6501", "2018", "1").Result;
        }
    }
}
