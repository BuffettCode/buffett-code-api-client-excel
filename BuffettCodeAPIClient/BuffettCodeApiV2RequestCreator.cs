using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2RequestCreator
    {
        public static ApiGetRequest CreateGetQuarterRequest(string ticker, uint fiscalYear, uint fiscalQuarter, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
            FiscalYearValidator.Validate(fiscalYear);
            FiscalQuarterValidator.Validate(fiscalQuarter);
            var paramaters = new Dictionary<string, string>()
            {
                {"ticker", ticker },
                {"fy", fiscalYear.ToString() },
                {"fq", fiscalQuarter.ToString() },
            };

            var endpoint = useOndemand ?
                BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV2Config.ENDPOINT_QUARTER;

            return new ApiGetRequest(endpoint, paramaters);
        }

        public static ApiGetRequest CreateGetQuarterRangeRequest(string ticker, string from, string to)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"tickers", ticker },
                {"from", from },
                {"to", to },
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

    }
}