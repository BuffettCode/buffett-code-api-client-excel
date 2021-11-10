using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using System.Collections.Generic;



namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3RequestCreator
    {
        public static ApiGetRequest CreateGetDailyRequest(string ticker, DayPeriod day, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"ticker", ticker },
                {"date", day.ToString() },
            };

            var endpoint = useOndemand ?
                BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY : BuffettCodeApiV3Config.ENDPOINT_DAILY;

            return new ApiGetRequest(endpoint, paramaters);
        }

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
                BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV3Config.ENDPOINT_QUARTER;

            return new ApiGetRequest(endpoint, paramaters);
        }


        public static ApiGetRequest CreateGetQuarterRangeRequest(string ticker, FiscalQuarterPeriod from, FiscalQuarterPeriod to)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"ticker", ticker },
                {"from", from.ToString() },
                {"to", to.ToString() },
            };
            return new ApiGetRequest(BuffettCodeApiV3Config.ENDPOINT_BULK_QUARTER, paramaters);
        }
        public static ApiGetRequest CreateGetCompanyRequest(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"ticker", ticker},
            };

            return new ApiGetRequest(BuffettCodeApiV3Config.ENDPOINT_COMPANY, paramaters);
        }

    }
}