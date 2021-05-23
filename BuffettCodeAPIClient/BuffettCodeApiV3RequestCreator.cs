using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;



namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3RequestCreator
    {
        public static ApiGetRequest CreateGetDailyRequest(string ticker, DateTime date, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
            {
                {"ticker", ticker },
                {"date", date.Date.ToString("yyyy-MM-dd") },
            };

            var endpoint = useOndemand ?
                BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY : BuffettCodeApiV3Config.ENDPOINT_DAILY;

            return new ApiGetRequest(endpoint, paramaters);
        }
    }
}