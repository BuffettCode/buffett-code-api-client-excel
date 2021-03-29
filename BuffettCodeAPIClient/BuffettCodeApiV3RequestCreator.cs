﻿using System;
using System.Collections.Generic;
using BuffettCodeCommon.Validator;
using BuffettCodeAPIClient.Config;



namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3RequestCreator
    {
        public static (string, Dictionary<string, string>) CreateGetDailyRequest(string ticker, DateTime date, bool useOndemand)
        {
            JpTickerValidator.Validate(ticker);
           var paramaters = new Dictionary<string, string>()
             {
                 {"ticker", ticker },
                 {"date", date.Date.ToString("yyyy-MM-dd") },
             };
 
            var endpoint = useOndemand ?
                BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY : BuffettCodeApiV3Config.ENDPOINT_DAILY;

            return (endpoint, paramaters);
        }
    }
}