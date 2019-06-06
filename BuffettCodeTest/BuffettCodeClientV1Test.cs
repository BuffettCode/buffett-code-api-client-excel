using BuffettCodeAddin;
using BuffettCodeAddin.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Buffett
{
    [TestClass]
    public class BuffettCodeClientV1Test
    {
        [TestMethod]
        public void TestGetQuarter()
        {
            var client = new BuffettCodeClientV1();
            var json = client.GetQuarter(BuffettCodeTestUtils.GetValidApiKey(), "6501", "2018", "4").Result;
            var quarters = Quarter.Parse("6501", json);

            Assert.IsFalse(quarters.Count == 0);
            Assert.AreEqual("6501", quarters[0].Ticker);
            Assert.IsNotNull(quarters[0].GetValue("assets")); // 値は変わりうるのでNullかどうかだけチェック
            Assert.IsNull(quarters[0].GetDescription("assets"));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestGetIndicator()
        {
            var client = new BuffettCodeClientV1();
            var json = client.GetIndicator(BuffettCodeTestUtils.GetValidApiKey(), "6501").Result;
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
