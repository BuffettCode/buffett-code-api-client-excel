using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
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
            // ok case
            var ticker = "6591";
            uint fiscalYear = 2019;
            uint fiscalQuarter = 3;
            var period = FiscalQuarterPeriod.Create(fiscalYear, fiscalQuarter);

            // use ondemand
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, period, true);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);


            // not use ondemand
            request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, period, false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);

            // latest case
            request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, RelativeFiscalQuarterPeriod.CreateLatest(), false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("LY", request.Parameters["fy"]);
            Assert.AreEqual("LQ", request.Parameters["fq"]);

            // relative case
            request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, RelativeFiscalQuarterPeriod.Create(1, 2), false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("LY-1", request.Parameters["fy"]);
            Assert.AreEqual("LQ-2", request.Parameters["fq"]);

            // validation Errors
            Assert.ThrowsException<ValidationError>(
                () =>
                BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest("aaa", period, false)
                );

        }

        [TestMethod()]
        public void CreateGetIndicatorRequestTest()
        {
            // ok case
            var ticker = "6501";
            var request = BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest(ticker);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_INDICATOR);
            Assert.AreEqual(ticker, request.Parameters["tickers"]);

            // validation errors
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest("aaa"));
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

            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest(ticker, from, to);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["tickers"]);
            Assert.AreEqual(fromStr, request.Parameters["from"]);
            Assert.AreEqual(toStr, request.Parameters["to"]);

            // validation errors
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest("aaa", from, to));
        }

        [TestMethod()]
        public void CreateGetCompanyRequestTest()
        {
            // ok case
            var ticker = "1234";
            var request = BuffettCodeApiV2RequestCreator.CreateGetCompanyRequest(ticker);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_COMPANY);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);

            // validation errors
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV2RequestCreator.CreateGetCompanyRequest("aaa"));
        }
    }
}