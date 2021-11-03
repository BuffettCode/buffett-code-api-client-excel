using BuffettCodeCommon.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient.Tests
{
    class MockApiclientCore : IApiClientCore
    {
        private string apiKey;
        public MockApiclientCore(string apiKey)
        {
            this.apiKey = apiKey;
        }


        public Task<string> Get(ApiGetRequest request, bool isConfigureAwait)
        {
            return Task<string>.FromResult(request.ToString());
        }

        public string GetApiKey() => this.apiKey;
        public IApiClientCore SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
            return this;
        }
    }

    class ErrorMockApiClientCore : IApiClientCore
    {
        private string apiKey;
        public ErrorMockApiClientCore(string apiKey)
        {
            this.apiKey = apiKey;
        }


        public Task<string> Get(ApiGetRequest request, bool isConfigureAwait)
        {
            throw new BuffettCodeApiClientException();
        }

        public string GetApiKey() => this.apiKey;
        public IApiClientCore SetApiKey(string apikey)
        {
            apiKey = apikey;
            return this;
        }
    }

    [TestClass()]
    public class ApiClientCoreWithCacheTests
    {
        private static ApiRequestCacheHelper CreateCheHelperForTest()
        {
            var cacheKey = new Random().Next().GetHashCode().ToString();
            return new ApiRequestCacheHelper(new MemoryCache(cacheKey), new Uri("http://example.com"));
        }

        [TestMethod()]
        public void GetAndUpdateApiKeyTest()
        {
            var initApiKey = @"init-key";
            var newApiKey = @"new-key";
            var mockApiClientCore = new MockApiclientCore(initApiKey);
            var client = ApiClientCoreWithCache.Create(mockApiClientCore, CreateCheHelperForTest());

            // test init value
            Assert.AreEqual(initApiKey, client.GetApiKey());

            client.UpdateApiKey(newApiKey);
            Assert.AreEqual(newApiKey, client.GetApiKey());
        }

        [TestMethod()]
        public void GetTest()
        {
            var mockApiClientCore = new MockApiclientCore("dummy");
            var client = ApiClientCoreWithCache.Create(mockApiClientCore, CreateCheHelperForTest());
            ApiGetRequest request = new ApiGetRequest("dummy endpoint", new Dictionary<string, string>());
            Assert.AreEqual(request.ToString(), client.Get(request, false, true));
            // use cache
            Assert.AreEqual(request.ToString(), client.Get(request, false, true));

            // don't use cache
            Assert.AreEqual(request.ToString(), client.Get(request, false, false));
        }

        [TestMethod()]
        public void GetExceptionTest()
        {
            var mockApiClientCore = new ErrorMockApiClientCore("dummy");
            var client = ApiClientCoreWithCache.Create(mockApiClientCore, CreateCheHelperForTest());
            ApiGetRequest request = new ApiGetRequest("dummy endpoint", new Dictionary<string, string>());
            Assert.ThrowsException<BuffettCodeApiClientException>(() => client.Get(request, false, false));
        }
    }
}