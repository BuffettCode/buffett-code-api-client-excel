using BuffettCodeAPIClient;
using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Validator;
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
            var (endpoint, param) = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, fiscalYear, fiscalQuarter, true);
            Assert.AreEqual(endpoint, BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER);
            Assert.AreEqual(ticker, param["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), param["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), param["fq"]);


            // not use ondemand
            (endpoint, param) = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, fiscalYear, fiscalQuarter, false);
            Assert.AreEqual(endpoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, param["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), param["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), param["fq"]);


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
            var (endpoint, param) = BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest(ticker);
            Assert.AreEqual(endpoint, BuffettCodeApiV2Config.ENDPOINT_INDICATOR);
            Assert.AreEqual(ticker, param["tickers"]);

            // validation errors
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest("aaa"));
        }
    }
}