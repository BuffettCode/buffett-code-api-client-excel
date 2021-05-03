using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiResponseValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {
            // ok case
            ApiResponseValidator.Validate(new JObject { ["message"] = "ok" });

            // ng case
            var error = ApiErrorMessageConfig.TEST_API_CONSTRAINT + "dummy";
            Assert.ThrowsException<TestAPIConstraintException>(() => ApiResponseValidator.Validate(new JObject { ["message"] = error }));
        }
    }
}