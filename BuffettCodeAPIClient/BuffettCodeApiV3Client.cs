using BuffettCodeCommon.Config;
using BuffettCodeCommon.Validator;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;


namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCoreWithCache apiClientCore;
        private static readonly BuffettCodeApiV3Client instance = new BuffettCodeApiV3Client();
        private readonly MemoryCache cache = new MemoryCache(nameof(BuffettCodeApiV3Client));

        private BuffettCodeApiV3Client()
        {
            apiClientCore = ApiClientCoreWithCache.Create(
                BuffettCodeApiKeyConfig.TestApiKey,
                BuffettCodeApiV3Config.BASE_URL,
                cache
            );


        }

        public async Task<JObject> GetDaily(string ticker, DateTime day, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, useOndemand);
            JpTickerValidator.Validate(ticker);
            var response = await apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public void ClearCache() => apiClientCore.ClearCache();

        public void UpdateApiKey(string apiKey) => apiClientCore.UpdateApiKey(apiKey);

        public string GetApiKey() => apiClientCore.GetApiKey();

        public static BuffettCodeApiV3Client GetInstance(string apiKey)
        {
            instance.UpdateApiKey(apiKey);
            return instance;
        }

        ~BuffettCodeApiV3Client()
        {
            cache.Dispose();
        }
    }
}