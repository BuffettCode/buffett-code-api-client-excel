using BuffettCodeCommon.Config;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2RequestCreator
    {
        public static ApiGetRequest CreateGetQuarterRequest(TickerQuarterParameter parameter, bool useOndemand)
        {
            var endpoint = useOndemand ? BuffettCodeApiV2Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV2Config.ENDPOINT_QUARTER;
            return new ApiGetRequest(endpoint, parameter.ToApiV2Parameters());
        }


        public static ApiGetRequest CreateGetQuarterRangeRequest(TickerPeriodRangeParameter parameter)
        {
            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_QUARTER, parameter.ToApiV2Parameters());
        }

        public static ApiGetRequest CreateGetIndicatorRequest(TickerEmptyPeriodParameter parameter)
        {
            // Indicator のみ、"ticker"を"tickers" で渡す必要があるのでここで書き換える
            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_INDICATOR, new Dictionary<string, string>() { { ApiRequestParamConfig.KeyTickers, parameter.GetTicker() } });
        }
        public static ApiGetRequest CreateGetCompanyRequest(TickerEmptyPeriodParameter parameter)
        {
            return new ApiGetRequest(BuffettCodeApiV2Config.ENDPOINT_COMPANY, parameter.ToApiV2Parameters());
        }

    }
}