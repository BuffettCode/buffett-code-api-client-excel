using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;


namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiGetResponseValidatorTests
    {
        [TestMethod()]
        public void ValidateTest()
        {

            // valid case
            ApiGetResponseValidator.Validate(new JObject { ["message"] = "ok" });

            // invalid case
            var error = ApiErrorMessageConfig.TEST_API_CONSTRAINT + "dummy";
            Assert.ThrowsException<TestAPIConstraintException>(() => ApiGetResponseValidator.Validate(new JObject { ["message"] = error }));
        }
    }
}