using System.Collections.Generic;
using BuffettCodeCommon.Validator;
using BuffettCodeAPIClient.Config;



namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2RequestCreator
    {
        public static (string, Dictionary<string, string>) CreateGetQuarterRequest(string ticker, uint fiscalYear, uint fiscalQuarter, bool useOndemand)
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

            return (endpoint, paramaters);
        }
        public static (string, Dictionary<string, string>) CreateGetIndicatorRequest(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            var paramaters = new Dictionary<string, string>()
             {
                 {"tickers", ticker },
             };

            return (BuffettCodeApiV2Config.ENDPOINT_INDICATOR, paramaters);
        }

    }
}
