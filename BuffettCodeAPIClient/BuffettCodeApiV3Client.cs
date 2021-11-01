using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using Newtonsoft.Json.Linq;
using System.Runtime.Caching;


namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCoreWithCache apiClientCore;
        private static readonly BuffettCodeApiV3Client instance = new BuffettCodeApiV3Client();
        private readonly MemoryCache cache = BuffettCodeAddinCache.GetInstance();

        private BuffettCodeApiV3Client()
        {
            apiClientCore = ApiClientCoreWithCache.Create(
                BuffettCodeApiKeyConfig.TestApiKey,
                BuffettCodeApiV3Config.BASE_URL,
                cache
            );


        }

        public JObject GetDaily(string ticker, DayPeriod day, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, useOndemand);
            JpTickerValidator.Validate(ticker);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response.Result);
        }

        public void UpdateApiKey(string apiKey) => apiClientCore.UpdateApiKey(apiKey);

        public string GetApiKey() => apiClientCore.GetApiKey();

        public static BuffettCodeApiV3Client GetInstance(string apiKey)
        {
            instance.UpdateApiKey(apiKey);
            return instance;
        }

        public JObject Get(DataTypeConfig dataType, string ticker, IPeriod period, bool useOndemand, bool isConfigureAwait, bool useCache)
        {
            switch (dataType)
            {
                case DataTypeConfig.Daily:
                    return GetDaily(ticker, (DayPeriod)period, useOndemand, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V3");
            }
        }

        public JObject GetRange(DataTypeConfig dataType, string ticker, IPeriod from, IPeriod to, bool useOndemand, bool isConfigureAwait, bool useCache)
        {
            switch (dataType)
            {
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V3");
            }
        }
    }
}