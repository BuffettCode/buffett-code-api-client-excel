using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
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

        public JObject GetDaily(string ticker, IDailyPeriod day, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, useOndemand);
            JpTickerValidator.Validate(ticker);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public JObject GetQuarter(string ticker, IQuarterlyPeriod period, bool useOndemand, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRequest(ticker, period, useOndemand);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public JObject GetQuarterRange(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetQuarterRangeRequest(ticker, from, to);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }

        public void UpdateApiKey(string apiKey) => apiClientCore.UpdateApiKey(apiKey);

        public string GetApiKey() => apiClientCore.GetApiKey();

        public JObject Get(DataTypeConfig dataType, string ticker, IPeriod period, bool useOndemand, bool isConfigureAwait, bool useCache)
        {
            switch (dataType)
            {
                case DataTypeConfig.Daily:
                    return GetDaily(ticker, (IDailyPeriod)period, useOndemand, isConfigureAwait, useCache);
                case DataTypeConfig.Quarter:
                    return GetQuarter(ticker, (IQuarterlyPeriod)period, useOndemand, isConfigureAwait, useCache);
                case DataTypeConfig.Company:
                    return GetCompany(ticker, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V3");
            }
        }

        public JObject GetRange(DataTypeConfig dataType, string ticker, IPeriod from, IPeriod to, bool useOndemand, bool isConfigureAwait, bool useCache)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return GetQuarterRange(ticker, (FiscalQuarterPeriod)from, (FiscalQuarterPeriod)to, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"Get {dataType} is not supported at V3");
            }
        }
        public JObject GetCompany(string ticker, bool isConfigureAwait = true, bool useCache = true)
        {
            var request = BuffettCodeApiV3RequestCreator.CreateGetCompanyRequest(ticker);
            var response = apiClientCore.Get(request, isConfigureAwait, useCache);
            return ApiGetResponseBodyParser.Parse(response);
        }


    }
}