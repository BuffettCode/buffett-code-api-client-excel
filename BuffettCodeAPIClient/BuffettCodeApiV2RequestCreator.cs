using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2RequestCreator
    {
        public static ApiGetRequest CreateGetQuarterRequest(string ticker, FiscalQuarterPeriod period, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"ticker", ticker },
                {"fy", period.Year.ToString() },
                {"fq", period.Quarter.ToString() },
            };

            var endpoint = useOndemand ?
                BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV2Config.ENDPOINT_QUARTER;

            return new ApiGetRequest(endpoint, paramaters);
        }

        public static ApiGetRequest CreateGetQuarterRangeRequest(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"tickers", ticker },
                {"from", from.ToString() },
                {"to", to.ToString() },
            };
            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_QUARTER, paramaters);
        }

        public static ApiGetRequest CreateGetIndicatorRequest(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"tickers", ticker },
            };

            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_INDICATOR, paramaters);
        }
        public static ApiGetRequest CreateGetCompanyRequest(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"ticker", ticker },
            };

            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_COMPANY, paramaters);
        }

    }
}