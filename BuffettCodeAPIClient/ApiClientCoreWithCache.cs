using System;
using System.Runtime.Caching;

namespace BuffettCodeAPIClient
{
    public class ApiClientCoreWithCache
    {
        private readonly IApiClientCore apiClientCore;
        private readonly ApiRequestCacheHelper cacheHelper;

        private ApiClientCoreWithCache(IApiClientCore apiClientCore
            , ApiRequestCacheHelper cacheHelper)
        {
            this.apiClientCore = apiClientCore;
            this.cacheHelper = cacheHelper;
        }

        public static ApiClientCoreWithCache Create(string apiKey, string baseUrl, MemoryCache cache)
        {
            var baseUri = new Uri(baseUrl);
            var apiClientCore = new ApiClientCore(apiKey, baseUri);
            var cacheHelper = new ApiRequestCacheHelper(cache, baseUri);
            return new ApiClientCoreWithCache(apiClientCore, cacheHelper);
        }

        public static ApiClientCoreWithCache Create(IApiClientCore apiClientCore, ApiRequestCacheHelper cacheHelper)
        {
            return new ApiClientCoreWithCache(apiClientCore, cacheHelper);
        }

        public void UpdateApiKey(string apiKey) => this.apiClientCore.SetApiKey(apiKey);

        public string GetApiKey() => this.apiClientCore.GetApiKey();

        public string Get(ApiGetRequest request, bool isConfigureAwait, bool useCache)
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