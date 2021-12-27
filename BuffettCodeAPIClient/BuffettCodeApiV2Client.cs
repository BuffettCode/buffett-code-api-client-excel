using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using Newtonsoft.Json.Linq;

namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCoreWithCache apiClientCore;


        public BuffettCodeApiV2Client(ApiClientCoreWithCache apiClientCore)
        {
            this.apiClientCore = apiClientCore;
        }

        public JObject GetQuarter(TickerQuarterParameter parameter, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(parameter, useOndemand);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public JObject GetIndicator(TickerEmptyPeriodParameter parameter, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest
                (parameter);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public JObject GetQuarterRange(TickerPeriodRangeParameter parameter, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetQuarterRangeRequest(parameter);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }
        public JObject GetCompany(TickerEmptyPeriodParameter parameter, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV2RequestCreator.CreateGetCompanyRequest(parameter);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public void UpdateApiKey(string apiKey) => apiClientCore.UpdateApiKey(apiKey);

        public string GetApiKey() => apiClientCore.GetApiKey();

        public JObject Get(DataTypeConfig dataType, ITickerPeriodParameter parameter, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return GetQuarter(parameter as TickerQuarterParameter, useOndemand, isConfigureAwait, useCache);
                case DataTypeConfig.Indicator:
                    return GetIndicator(parameter as TickerEmptyPeriodParameter, isConfigureAwait, useCache);
                case DataTypeConfig.Company:
                    return GetCompany(parameter as TickerEmptyPeriodParameter, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V2");
            }
        }

        public JObject GetRange(DataTypeConfig dataType, TickerPeriodRangeParameter paramter, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return GetQuarterRange(
                        paramter, useOndemand, isConfigureAwait, useCache
                    );
                default:
                    throw new NotSupportedDataTypeException($"GetRnage {dataType} is not supported at V2");
            }
        }
    }
}