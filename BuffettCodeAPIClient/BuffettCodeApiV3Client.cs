using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using Newtonsoft.Json.Linq;


namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCoreWithCache apiClientCore;
        public BuffettCodeApiV3Client(ApiClientCoreWithCache apiClientCore)
        {
            this.apiClientCore = apiClientCore;
        }

        public JObject GetDaily(TickerDayParameter parameter, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(parameter, useOndemand);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public JObject GetQuarter(TickerQuarterParameter parameter, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest(parameter, useOndemand);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public JObject GetQuarterRange(TickerPeriodRangeParameter parameter, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRangeRequest(parameter);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public JObject GetMonthly(TickerYearMonthParameter parameter, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetMonthlyRequest(parameter);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);

        }
        public void UpdateApiKey(string apiKey) => apiClientCore.UpdateApiKey(apiKey);

        public string GetApiKey() => apiClientCore.GetApiKey();

        public JObject Get(DataTypeConfig dataType, ITickerIntentParameter parameter, bool useOndemand, bool isConfigureAwait, bool useCache)
        {
            switch (dataType)
            {
                case DataTypeConfig.Daily:
                    return GetDaily(parameter as TickerDayParameter, useOndemand, isConfigureAwait, useCache);
                case DataTypeConfig.Quarter:
                    return GetQuarter(parameter as TickerQuarterParameter, useOndemand, isConfigureAwait, useCache);
                case DataTypeConfig.Monthly:
                    return GetMonthly(parameter as TickerYearMonthParameter, isConfigureAwait, useCache);
                case DataTypeConfig.Company:
                    return GetCompany(parameter as TickerEmptyIntentParameter, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V3");
            }
        }

        public JObject GetRange(DataTypeConfig dataType, TickerPeriodRangeParameter parameter, bool useOndemand, bool isConfigureAwait, bool useCache)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return GetQuarterRange(parameter, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V3");
            }
        }
        public JObject GetCompany(TickerEmptyIntentParameter parameter, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetCompanyRequest(parameter);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }


    }
}