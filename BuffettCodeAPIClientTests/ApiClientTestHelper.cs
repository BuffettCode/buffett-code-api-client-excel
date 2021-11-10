using System;
using System.Runtime.Caching;

namespace BuffettCodeAPIClient.Tests
{
    class ApiClientTestHelper
    {
        private static string GenerateCacheKey() => new Random().Next().GetHashCode().ToString();

        public static MemoryCache CreateCacheForCache() => new MemoryCache(GenerateCacheKey());

        public static ApiRequestCacheHelper CreateCheHelperForTest() =>
         new ApiRequestCacheHelper(CreateCacheForCache(), new Uri("http://example.com"));

        public static ApiClientCoreWithCache CreateMockApiClientCoreWithCache(string apiKey)
        {
            var clientCore = new MockApiClientCore(apiKey);
            var cacheHelper = CreateCheHelperForTest();
            return ApiClientCoreWithCache.Create(clientCore, cacheHelper);
        }

        public static ApiClientCoreWithCache CreateErrorMockApiClientCoreWithCache(string apiKey)
        {
            var clientCore = new ErrorMockApiClientCore(apiKey);
            var cacheHelper = CreateCheHelperForTest();
            return ApiClientCoreWithCache.Create(clientCore, cacheHelper);
        }
    }
}