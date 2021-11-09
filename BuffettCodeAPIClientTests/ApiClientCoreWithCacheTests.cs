using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiClientCoreWithCacheTests
    {
        private readonly ApiRequestCacheHelper cacheHelper = ApiClientTestHelper.CreateCheHelperForTest();

        [TestMethod()]
        public void GetAndUpdateApiKeyTest()
        {
            var initApiKey = @"init-key";
            var newApiKey = @"new-key";
            var mockApiClientCore = new MockApiClientCore(initApiKey);
            var client = ApiClientCoreWithCache.Create(mockApiClientCore, cacheHelper);

            // test init value
            Assert.AreEqual(initApiKey, client.GetApiKey());

            client.UpdateApiKey(newApiKey);
            Assert.AreEqual(newApiKey, client.GetApiKey());
        }

        [TestMethod()]
        public void GetTest()
        {
            var mockApiClientCore = new MockApiClientCore("dummy");
            var client = ApiClientCoreWithCache.Create(mockApiClientCore, cacheHelper);
            ApiGetRequest request = new ApiGetRequest("dummy endpoint", new Dictionary<string, string>());
            Assert.AreEqual(MockApiClientCore.Response, client.Get(request, false, true));
            // use cache
            Assert.AreEqual(MockApiClientCore.Response, client.Get(request, false, true));

            // don't use cache
            Assert.AreEqual(MockApiClientCore.Response, client.Get(request, false, false));
        }

        [TestMethod()]
        public void GetExceptionTest()
        {
            var mockApiClientCore = new ErrorMockApiClientCore("dummy");
            var client = ApiClientCoreWithCache.Create(mockApiClientCore, cacheHelper);
            ApiGetRequest request = new ApiGetRequest("dummy endpoint", new Dictionary<string, string>());
            Assert.ThrowsException<BuffettCodeApiClientException>(() => client.Get(request, false, false));
        }
    }
}