using BuffettCodeAddin.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuffettCodeAddin.Client.UnitTests
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
        public void TestGetQuarterByTestApiKey()
        {
            var client = new BuffettCodeClientV2();
            var json = client.GetQuarter(BuffettCodeTestUtils.GetTestApiKey(), "6501", "2018", "4").Result;
            var quarters = Quarter.Parse("6501", json);

            Assert.IsFalse(quarters.Count == 0);
            Assert.AreEqual("6501", quarters[0].Ticker);
            Assert.IsNotNull(quarters[0].GetValue("assets")); // 値は変わりうるのでNullかどうかだけチェック
            Assert.IsNotNull(quarters[0].GetDescription("assets"));
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestGetQuarterByTestApiKeyDenied()
        {
            // テスト用のAPIキーではゼロイチ銘柄以外は失敗し、 InvalidApiKeyException を投げる
            var invalid_token_request = new BuffettCodeClientV2().GetQuarter(BuffettCodeTestUtils.GetTestApiKey(), "6502", "2018", "4");
            try
            {
                invalid_token_request.Wait();
                Assert.Fail();
            }
            catch (AggregateException ae){
                foreach ( var e in ae.InnerExceptions)
                {
                    Assert.IsInstanceOfType(e, typeof(InvalidAPIKeyException));
                }
                throw ae;
            }
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
