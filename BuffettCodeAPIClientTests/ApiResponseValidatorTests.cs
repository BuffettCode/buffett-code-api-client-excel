using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuffettCodeAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BuffettCodeCommon.Exception;
using BuffettCodeAPIClient.Config;

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
            Assert.ThrowsException<TestAPIConstraintException>(() => ApiResponseValidator.Validate(new JObject {["message"] = error }));
        }
    }
}