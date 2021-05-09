using System;
using System.Runtime.Caching;

namespace BuffettCodeAPIClient
{
    public class ApiRequestCacheHelper
    {
        private readonly MemoryCache cache;
        private readonly CacheItemPolicy policy = new CacheItemPolicy();

        public ApiRequestCacheHelper(MemoryCache memoryCache)
        {
            cache = memoryCache;
        }
        public bool HasCache(ApiGetRequest request) => cache.Contains(CacheKeyCreator.Create(request));

        public object Get(ApiGetRequest request) => cache.Get(CacheKeyCreator.Create(request));

        public ApiRequestCacheHelper Set(ApiGetRequest request, String value)
        {
            cache.Set(CacheKeyCreator.Create(request), value, policy);
            return this;
        }

    }

}