using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class PeriodRangeParameterTests
    {
        [TestMethod()]
        public void ToApiV2ParametersTest()
        {
            var ticker = "1234";
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2020, 2);
            var parameter = TickerPeriodRangeParameter.Create(ticker, from, to).ToApiV2Parameters();
            Assert.AreEqual(ticker, parameter["tickers"]);
            Assert.AreEqual(from.ToString(), parameter["from"]);
            Assert.AreEqual(to.ToString(), parameter["to"]);
        }

        [TestMethod()]
        public void ToApiV3ParametersTest()
        {
            var ticker = "1234";
            var from = FiscalQuarterPeriod.Create(2020, 1);
            var to = FiscalQuarterPeriod.Create(2020, 2);
            var parameter = TickerPeriodRangeParameter.Create(ticker, from, to).ToApiV3Parameters();
            Assert.AreEqual(ticker, parameter["ticker"]);
            Assert.AreEqual(from.ToString(), parameter["from"]);
            Assert.AreEqual(to.ToString(), parameter["to"]);
        }
    }
}