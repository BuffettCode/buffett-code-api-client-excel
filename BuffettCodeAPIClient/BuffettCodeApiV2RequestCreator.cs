using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2RequestCreator
    {
        public static ApiGetRequest CreateGetQuarterRequest(string ticker, IQuarterlyPeriod period, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = period.ToV2Parameter();
            paramaters.Add(ApiRequestParamConfig.KeyTicker, ticker);
            var endpoint = useOndemand ? BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV2Config.ENDPOINT_QUARTER;
            return new ApiGetRequest(endpoint, paramaters);
        }


        public static ApiGetRequest CreateGetQuarterRangeRequest(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {ApiRequestParamConfig.KeyTickers, ticker },
                {ApiRequestParamConfig.KeyFrom, from.ToString() },
                {ApiRequestParamConfig.KeyTo, to.ToString() },
            };
            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_QUARTER, paramaters);
        }

        public static ApiGetRequest CreateGetIndicatorRequest(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {ApiRequestParamConfig.KeyTickers, ticker },
            };

            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_INDICATOR, paramaters);
        }
        public static ApiGetRequest CreateGetCompanyRequest(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {ApiRequestParamConfig.KeyTicker, ticker},
            };

            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_COMPANY, paramaters);
        }

    }
}