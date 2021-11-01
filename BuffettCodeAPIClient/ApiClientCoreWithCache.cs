using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient
{
    public class ApiClientCoreWithCache
    {
        private readonly ApiClientCore apiClientCore;
        private readonly ApiRequestCacheHelper cacheHelper;

        private ApiClientCoreWithCache(string apiKey, Uri baseUri, MemoryCache cache)
        {
            this.apiClientCore = new ApiClientCore(apiKey, baseUri);
            this.cacheHelper = new ApiRequestCacheHelper(cache, baseUri);
        }

        public static ApiClientCoreWithCache Create(string apiKey, string baseUrl, MemoryCache cache)
        {
            return new ApiClientCoreWithCache(apiKey, new Uri(baseUrl), cache);
        }

        public static ApiClientCoreWithCache Create(string apiKey, Uri baseUrl, MemoryCache cache)
        {
            return new ApiClientCoreWithCache(apiKey, baseUrl, cache);
        }

        public void UpdateApiKey(string apikey)
        {
            this.apiClientCore.ApiKey = apikey;
        }

        public string GetApiKey() => this.apiClientCore.ApiKey;

        public async Task<string> Get(ApiGetRequest request, bool isConfigureAwait, bool useCache)
        {
            if (useCache && cacheHelper.HasCache(request))
            {
                return (string)cacheHelper.Get(request);
            }
            else
            {
                return apiClientCore.Get(request, isConfigureAwait).Result;
            }
        }

    }
}