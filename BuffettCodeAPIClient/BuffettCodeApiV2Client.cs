using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCoreWithCache apiClientCore;
        private static readonly BuffettCodeApiV2Client instance = new BuffettCodeApiV2Client();
        private readonly MemoryCache cache = BuffettCodeAddinCache.GetInstance();


        private BuffettCodeApiV2Client()
        {
            apiClientCore = ApiClientCoreWithCache.Create(
                BuffettCodeApiKeyConfig.TestApiKey,
                BuffettCodeApiV2Config.BASE_URL,
                cache
            );
        }

        public async Task<JObject> GetQuarter(string ticker, FiscalQuarterPeriod period, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, period, useOndemand);
            var response = await apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public async Task<JObject> GetIndicator(string ticker, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest
                (ticker);
            var response = await apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public async Task<JObject> GetQuarterRange(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest(ticker, from, to);
            var response = await apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public void UpdateApiKey(string apiKey) => apiClientCore.UpdateApiKey(apiKey);

        public string GetApiKey() => apiClientCore.GetApiKey();

        public static BuffettCodeApiV2Client GetInstance(string apiKey)
        {
            instance.UpdateApiKey(apiKey);
            return instance;
        }

        public Task<JObject> Get(DataTypeConfig dataType, string ticker, IPeriod period, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return GetQuarter(ticker, (FiscalQuarterPeriod)period, useOndemand, isConfigureAwait, useCache);
                case DataTypeConfig.Indicator:
                    return GetIndicator(ticker, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V2");
            }
        }

        public Task<JObject> GetRange(DataTypeConfig dataType, string ticker, IPeriod from, IPeriod to, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    if (!(from is FiscalQuarterPeriod && to is FiscalQuarterPeriod))
                    {
                        throw new ArgumentException($"both of from and to should be {typeof(FiscalQuarterPeriod)}");
                    }
                    return GetQuarterRange(
                        ticker, (FiscalQuarterPeriod)from, (FiscalQuarterPeriod)to, useOndemand, isConfigureAwait, useCache
                    );
                default:
                    throw new NotSupportedDataTypeException($"GetRnage {dataType} is not supported at V2");
            }
        }
    }
}