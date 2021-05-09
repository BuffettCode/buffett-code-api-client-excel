using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Caching;

namespace BuffettCodeAPIClient.Tests
{
    [TestClass()]
    public class ApiClientCoreWithCacheTests
    {
        [TestMethod()]
        public void GetAndUpdateApiKeyTest()
        {
            var initApiKey = @"init-key";
            var newApiKey = @"new-key";
            var client = ApiClientCoreWithCache.Create(initApiKey, @"http://example.com", new MemoryCache(@"GetAndUpdateApiKeyTest"));

            // test init value
            Assert.AreEqual(initApiKey, client.GetApiKey());

            client.UpdateApiKey(newApiKey);
            Assert.AreEqual(newApiKey, client.GetApiKey());
        }
    }
}