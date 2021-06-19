using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
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

            // use ondemand
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, fiscalYear, fiscalQuarter, true);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);


            // not use ondemand
            request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, fiscalYear, fiscalQuarter, false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);


            // validation Errors
            Assert.ThrowsException<ValidationError>(
                () =>
                BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest("aaa", fiscalYear, fiscalQuarter, false)
                );
            Assert.ThrowsException<ValidationError>(
                () =>
                BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, 1, fiscalQuarter, false)
                );
            Assert.ThrowsException<ValidationError>(
                () =>
                BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, fiscalYear, 10, false)
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
            var from = "2020Q1";
            var to = "2022Q4";

            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest(ticker, from, to);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["tickers"]);
            Assert.AreEqual(from, request.Parameters["from"]);
            Assert.AreEqual(to, request.Parameters["to"]);

            // validation errors
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest("aaa", from, to));

        }
    }
}