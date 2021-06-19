using BuffettCodeCommon.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiClientInstanceGetterTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // v2
            Assert.AreEqual(ApiClinentInstanceGetter.Get(BuffettCodeApiVersion.Version2, BuffettCodeApiKeyConfig.TestApiKey), BuffettCodeApiV2Client.GetInstance(BuffettCodeApiKeyConfig.TestApiKey));

            // v3
            Assert.AreEqual(ApiClinentInstanceGetter.Get(BuffettCodeApiVersion.Version3, BuffettCodeApiKeyConfig.TestApiKey), BuffettCodeApiV3Client.GetInstance(BuffettCodeApiKeyConfig.TestApiKey));

        }
    }
}