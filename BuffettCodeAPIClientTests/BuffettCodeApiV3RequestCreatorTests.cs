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

            // validation error
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV3RequestCreator.CreateGetDailyRequest("aa", day, false));
        }
    }
}