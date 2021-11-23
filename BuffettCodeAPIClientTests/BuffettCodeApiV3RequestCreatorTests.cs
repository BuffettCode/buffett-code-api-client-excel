using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class BuffettCodeApiV3RequestCreatorTests
    {
        [TestMethod()]
        public void CreateGetDailyRequestTest()
        {
            // use ondemand
            var ticker = "6501";
            var day = DayPeriod.Create(2021, 1, 1);
            var request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, true);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY, request.EndPoint);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("2021-01-01", request.Parameters["date"]);

            // not use ondemand
            request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, false);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_DAILY, request.EndPoint);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("2021-01-01", request.Parameters["date"]);

            // latest case
            request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, LatestDayPeriod.GetInstance(), false);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_DAILY, request.EndPoint);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("latest", request.Parameters["date"]);



            // validation error
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV3RequestCreator.CreateGetDailyRequest("aa", day, false));
        }

        [TestMethod()]
        public void CreateGetQuarterRequestTest()
        {
            var ticker = "6591";
            uint fiscalYear = 2019;
            uint fiscalQuarter = 3;
            var period = FiscalQuarterPeriod.Create(fiscalYear, fiscalQuarter);

            // use ondemand
            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest(ticker, period, true);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);

            // not use ondemand
            request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest(ticker, period, false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);

            // latest case
            request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest(ticker, LatestFiscalQuarterPeriod.GetInstance(), false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV2Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("LY", request.Parameters["fy"]);
            Assert.AreEqual("LQ", request.Parameters["fq"]);

            // validation Errors
            Assert.ThrowsException<ValidationError>(
                () =>
                BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest("aaa", period, false)
                );

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

            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRangeRequest(ticker, from, to);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_BULK_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fromStr, request.Parameters["from"]);
            Assert.AreEqual(toStr, request.Parameters["to"]);

            // validation errors
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV3RequestCreator.CreateGetQuarterRangeRequest("aaa", from, to));
        }

        [TestMethod()]
        public void CreateGetCompanyRequestTest()
        {
            // ok case
            var ticker = "1234";
            var request = BuffettCodeApiV3RequestCreator.CreateGetCompanyRequest(ticker);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_COMPANY);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);

            // validation errors
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV3RequestCreator.CreateGetCompanyRequest("aaa"));
        }
    }
}