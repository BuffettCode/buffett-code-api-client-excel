using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuffettCodeAPIClient;
using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var day = new DateTime(2021, 1, 1);
            var (endpoint, paramater) = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, true);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY, endpoint); 
            Assert.AreEqual(ticker, paramater["ticker"]); 
            Assert.AreEqual("2021-01-01", paramater["date"]); 

            // not use ondemand
            (endpoint, paramater) = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, false);
            Assert.AreEqual(BuffettCodeApiV3Config.ENDPOINT_DAILY, endpoint); 
            Assert.AreEqual(ticker, paramater["ticker"]); 
            Assert.AreEqual("2021-01-01", paramater["date"]);

            // validation error
            Assert.ThrowsException<ValidationError>(() => BuffettCodeApiV3RequestCreator.CreateGetDailyRequest("aa", day, false));
        }
    }
}