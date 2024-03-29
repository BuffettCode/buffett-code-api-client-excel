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
            // v3
            var clientV3 = ApiClientFactory.Create(BuffettCodeApiVersion.Version3, BuffettCodeApiKeyConfig.TestApiKey);

            Assert.IsInstanceOfType(clientV3, typeof(BuffettCodeApiV3Client));
            Assert.AreEqual(clientV3.GetApiKey(), BuffettCodeApiKeyConfig.TestApiKey);

            // api key validation error
            Assert.ThrowsException<ValidationError>(() => ApiClientFactory.Create(BuffettCodeApiVersion.Version3, "dummy"));
        }
    }
}