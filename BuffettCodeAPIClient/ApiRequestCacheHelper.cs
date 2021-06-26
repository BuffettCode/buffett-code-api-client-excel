using System;
using System.Runtime.Caching;

namespace BuffettCodeAPIClient
{
    public class ApiRequestCacheHelper
    {
        private readonly MemoryCache cache;
        private readonly string baseUrl;
        private readonly CacheItemPolicy policy = new CacheItemPolicy();

        public ApiRequestCacheHelper(MemoryCache memoryCache, Uri baseUri)
        {
            this.baseUrl = baseUri.ToString();
            this.cache = memoryCache;
        }
        public bool HasCache(ApiGetRequest request) => cache.Contains(CacheKeyCreator.Create(baseUrl, request));

        public object Get(ApiGetRequest request) => cache.Get(CacheKeyCreator.Create(baseUrl, request));

        public ApiRequestCacheHelper Set(ApiGetRequest request, String value)
        {
            cache.Set(CacheKeyCreator.Create(baseUrl, request), value, policy);
            return this;
        }

    }

}