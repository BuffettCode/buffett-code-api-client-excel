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
    }
}