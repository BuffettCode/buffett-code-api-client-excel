using BuffettCodeCommon.Config;
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
            var parameter = TickerDayParameter.Create(ticker, DayPeriod.Create(2021, 1, 1));
            var request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(parameter, true);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY, request.EndPoint);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("2021-01-01", request.Parameters["date"]);

            // not use ondemand
            request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(parameter, false);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_DAILY, request.EndPoint);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("2021-01-01", request.Parameters["date"]);

            // latest case
            request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(TickerDayParameter.Create(ticker, LatestDayPeriod.GetInstance()), false);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_DAILY, request.EndPoint);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual("latest", request.Parameters["date"]);
        }

        [TestMethod()]
        public void CreateGetQuarterRequestTest()
        {
            var ticker = "6591";
            uint fiscalYear = 2019;
            uint fiscalQuarter = 3;
            var parameter = TickerQuarterParameter.Create(ticker, FiscalQuarterPeriod.Create(fiscalYear, fiscalQuarter));

            // use ondemand
            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest(parameter, true);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);

            // not use ondemand
            request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest(parameter, false);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fiscalYear.ToString(), request.Parameters["fy"]);
            Assert.AreEqual(fiscalQuarter.ToString(), request.Parameters["fq"]);
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

            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRangeRequest(parameter);
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_BULK_QUARTER);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(fromStr, request.Parameters["from"]);
            Assert.AreEqual(toStr, request.Parameters["to"]);
        }

        [TestMethod()]
        public void CreateGetCompanyRequestTest()
        {
            var ticker = "1234";
            var request = BuffettCodeApiV3RequestCreator.CreateGetCompanyRequest(TickerEmptyIntentParameter.Create(ticker, Snapshot.GetInstance()));
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_COMPANY);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
        }

        [TestMethod()]
        public void CreateGetMonthlyRequestTest()
        {
            var ticker = "1234";
            var request = BuffettCodeApiV3RequestCreator.CreateGetMonthlyRequest(TickerYearMonthParameter.Create(ticker, YearMonthPeriod.Create(2019, 2)));
            Assert.AreEqual(request.EndPoint, BuffettCodeApiV3Config.ENDPOINT_MONTHLY);
            Assert.AreEqual(ticker, request.Parameters["ticker"]);
            Assert.AreEqual(2022, request.Parameters["year"]);
            Assert.AreEqual(8, request.Parameters["month"]);
        }
    }
}