using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class BuffettCodeApiV2RequestCreatorTests
    {
        [TestMethod()]
        public void CreateGetQuarterRequestTest()
        {
            var ticker = "6591";
            uint fiscalYear = 2019;
            uint fiscalQuarter = 3;
            var parameter = TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(fiscalYear, fiscalQuarter));

            // use ondemand
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(parameter, true);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);


            // not use ondemand
            request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(parameter, false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);

        }

        [TestMethod()]
        public void CreateGetIndicatorRequestTest()
        {
            var ticker = "6501";
            var parameter = TickerEmptyPeriodParameter.Create(ticker, Snapshot.GetInstance());
            var request = BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest(parameter);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_INDICATOR);
            Assert.AreEqual(ticker, request.Parameters["tickers"]);
        }

        [TestMethod()]
        public void CreateGetQuarterRangeRequestTest()
        {
            // ok case
            var ticker = "6501";
            var fromStr = "2020Q1";
            var toStr = "2022Q4";

            var from = FiscalQuarterPeriod.Parse(fromStr);
            var to = FiscalQuarterPeriod.Parse(toStr);
            var parameter = TickerPeriodRangeParameter.Create(ticker, from, to);

            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest(parameter);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["tickers"]);
            Assert.AreEqual(fromStr, request.Parameters["from"]);
            Assert.AreEqual(toStr, request.Parameters["to"]);
        }

        [TestMethod()]
        public void CreateGetCompanyRequestTest()
        {
            var ticker = "1234";
            var parameter = TickerEmptyPeriodParameter.Create(ticker, Snapshot.GetInstance());
            var request = BuffettCodeApiV2RequestCreator.CreateGetCompanyRequest(parameter);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_COMPANY);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
        }
    }
}