using BuffettCodeAPIClient.Config;
using BuffettCodeCommon;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;


namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCoreWithCache apiClientCore;
        private static readonly BuffettCodeApiV2Client instance = new BuffettCodeApiV2Client();
        private readonly MemoryCache cache = new MemoryCache(nameof(BuffettCodeApiV2Client));


        private BuffettCodeApiV2Client()
        {
            apiClientCore = ApiClientCoreWithCache.Create(
                Configuration.ApiKeyDefault,
                BuffettCodeApiV2Config.BASE_URL,
                cache
            );
        }

        public async Task<String> GetQuarter(string ticker, uint fiscalYear, uint fiscalQuarter, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, fiscalYear, fiscalQuarter, useOndemand);
            return await apiClientCore.Get(request, isConfigureAwait, useCache);
        }

        public async Task<String> GetIndicator(string ticker, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest
                (ticker);
            return await apiClientCore.Get(request, isConfigureAwait, useCache);
        }

        public async Task<String> GetQuarterRange(string ticker, string from, string to, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest(ticker, from, to);
            return await apiClientCore.Get(request, isConfigureAwait, useCache);
        }

        public void ClearCache() => apiClientCore.ClearCache();

        public void UpdateApiKey(string apiKey) => apiClientCore.UpdateApiKey(apiKey);

        public string GetApiKey() => apiClientCore.GetApiKey();

        public static BuffettCodeApiV2Client GetInstance(string apiKey)
        {
            instance.UpdateApiKey(apiKey);
            return instance;
        }

        ~BuffettCodeApiV2Client()
        {
            cache.Dispose();
        }

    }
}