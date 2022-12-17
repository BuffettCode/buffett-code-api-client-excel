using BuffettCodeCommon.Config;

namespace BuffettCodeExcelFunctions
{
    public class BCodeExecutor
    {
        private readonly ApiResourceFetcher fetcher;
        private readonly TickerIntentBuilder builder;

        public BCodeExecutor(BuffettCodeApiVersion version)
        {
            fetcher = new ApiResourceFetcher(version);
            builder = TickerIntentBuilder.Create(fetcher);
        }

        public string Execute(string ticker, DataTypeConfig dataType, string intent, string propertyName, bool isRawValue = false, bool isWithUnit = false)
        {
            var parameter = builder.SetTicker(ticker).SetIntent(intent).Build();
            var apiResource = fetcher.Fetch(dataType, parameter, true, true);
            return PropertySelector.SelectFormattedValue(propertyName, apiResource, isRawValue, isWithUnit, true);
        }
    }




}