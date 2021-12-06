using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;


namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3RequestCreator
    {
        private static string QuarterEndpoint(bool useOndemand) => useOndemand ? BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV2Config.ENDPOINT_QUARTER;

        public static ApiGetRequest CreateGetDailyRequest(string ticker, IDailyPeriod period, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = period.ToV3Parameter();
            paramaters.Add(ApiRequestParamConfig.KeyTicker, ticker);
            var endpoint = useOndemand ?
                BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY : BuffettCodeApiV3Config.ENDPOINT_DAILY;

            return new ApiGetRequest(endpoint, paramaters);
        }

        public static ApiGetRequest CreateGetQuarterRequest(string ticker, IQuarterlyPeriod period, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = period.ToV3Parameter();
            paramaters.Add(ApiRequestParamConfig.KeyTicker, ticker);

            var endpoint = useOndemand ?
                 BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV3Config.ENDPOINT_QUARTER;

            return new ApiGetRequest(endpoint, paramaters);
        }


        public static ApiGetRequest CreateGetQuarterRangeRequest(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {ApiRequestParamConfig.KeyTicker, ticker },
                {ApiRequestParamConfig.KeyFrom, from.ToString() },
                {ApiRequestParamConfig.KeyTo, to.ToString() },
            };
            return new ApiGetRequest(BuffettCodeApiV3Config.ENDPOINT_BULK_QUARTER, paramaters);
        }
        public static ApiGetRequest CreateGetCompanyRequest(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {ApiRequestParamConfig.KeyTicker, ticker},
            };

            return new ApiGetRequest(BuffettCodeApiV3Config.ENDPOINT_COMPANY, paramaters);
        }

    }
}