using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiRequestCacheHelperTests
    {
        [TestMethod()]
        public void TestCacheLifeCycle()
        {
            var cache = new MemoryCache("TestCacheLifeCycle");
            var baseUrl = "https://example.com/test";
            var cacheHelper = new ApiRequestCacheHelper(cache, new Uri(baseUrl));
            var endPoint = "dummy";
            var parameters = new Dictionary<string, string> { { "key", "value" } };
            var response = "results";
            var request = new ApiGetRequest(endPoint, parameters);

            // at first, no cache
            Assert.IsFalse(cacheHelper.HasCache(request));
            Assert.AreEqual(0, cache.GetCount());

            // set and get
            cacheHelper.Set(request, response);
            Assert.IsTrue(cacheHelper.HasCache(request));
            Assert.AreEqual(response, cacheHelper.Get(request));
            Assert.AreEqual(1, cache.GetCount());

            // dispose
            cache.Dispose();
            Assert.IsFalse(cacheHelper.HasCache(request));
        }
    }
}