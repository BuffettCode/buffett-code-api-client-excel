using BuffettCodeCommon.Config;


namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3RequestCreator
    {
        public static ApiGetRequest CreateGetDailyRequest(TickerDayParameter parameter, bool useOndemand)
        {
            var endpoint = useOndemand ?
                BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_DAILY : BuffettCodeApiV3Config.ENDPOINT_DAILY;

            return new ApiGetRequest(endpoint, parameter.ToApiV3Parameters());
        }

        public static ApiGetRequest CreateGetQuarterRequest(TickerQuarterParameter parameter, bool useOndemand)
        {
            var endpoint = useOndemand ?
                 BuffettCodeApiV3Config.ENDPOINT_ONDEMAND_QUARTER : BuffettCodeApiV3Config.ENDPOINT_QUARTER;

            return new ApiGetRequest(endpoint, parameter.ToApiV3Parameters());
        }


        public static ApiGetRequest CreateGetQuarterRangeRequest(TickerPeriodRangeParameter parameter)
        {
            return new ApiGetRequest(BuffettCodeApiV3Config.ENDPOINT_BULK_QUARTER, parameter.ToApiV3Parameters());
        }
        public static ApiGetRequest CreateGetCompanyRequest(TickerEmptyIntentParameter parameter)
        {
            return new ApiGetRequest(BuffettCodeApiV3Config.ENDPOINT_COMPANY, parameter.ToApiV3Parameters());
        }
        public static ApiGetRequest CreateGetMonthlyRequest(TickerYearMonthParameter parameter)
        {
            return new ApiGetRequest(BuffettCodeApiV3Config.ENDPOINT_MONTHLY, parameter.ToApiV3Parameters());
        }
    }
}