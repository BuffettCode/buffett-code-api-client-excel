using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiGetResponseReaderTests
    {
        [TestMethod()]
        public void ReadTest()
        {
            // ok case
            var response = new JObject { ["message"] = "ok" };
            Assert.AreEqual(response.ToString(), ApiGetResponseBodyParser.Parse(response.ToString()).ToString());

            // ng case
            var error = ApiErrorMessageConfig.TEST_API_CONSTRAINT + "dummy";
            var invalidResponse = new JObject { ["message"] = error };
            Assert.ThrowsException<TestAPIConstraintException>(() => ApiGetResponseBodyParser.Parse(invalidResponse.ToString()));
        }
    }
}