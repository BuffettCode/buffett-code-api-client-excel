using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;

using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiClientFactoryTest
    {
        [TestMethod()]
        public void CreateTest()
        {
            // v2
            var clientV2 = ApiClientFactory.Create(BuffettCodeApiVersion.Version2, BuffettCodeApiKeyConfig.TestApiKey);

            Assert.IsInstanceOfType(clientV2, typeof(BuffettCodeApiV2Client));
            Assert.AreEqual(clientV2.GetApiKey(), BuffettCodeApiKeyConfig.TestApiKey);
            // v3
            var clientV3 = ApiClientFactory.Create(BuffettCodeApiVersion.Version3, BuffettCodeApiKeyConfig.TestApiKey);

            Assert.IsInstanceOfType(clientV3, typeof(BuffettCodeApiV3Client));
            Assert.AreEqual(clientV3.GetApiKey(), BuffettCodeApiKeyConfig.TestApiKey);

            // api key validation error
            Assert.ThrowsException<ValidationError>(() => ApiClientFactory.Create(BuffettCodeApiVersion.Version2, "dummy"));
            Assert.ThrowsException<ValidationError>(() => ApiClientFactory.Create(BuffettCodeApiVersion.Version3, "dummy"));
        }
    }
}