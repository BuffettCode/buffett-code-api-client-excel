using BuffettCodeCommon.Config;

namespace BuffettCodeExcelFunctions
{
    public class BCodeExecutor
    {
        private readonly ApiResourceFetcher fetcher;
        private readonly TickerPiriodParameterBuilder builder;

        public BCodeExecutor(BuffettCodeApiVersion version)
        {
            fetcher = new ApiResourceFetcher(version);
            builder = TickerPiriodParameterBuilder.Create(fetcher);
        }

        public string Execute(string ticker, DataTypeConfig dataType, string periodParam, string propertyName, bool isRawValue = false, bool isWithUnit = false)
        {
            var parameter = builder.SetTicker(ticker).SetPeriodParam(periodParam).Build();
            var apiResource = fetcher.Fetch(dataType, parameter, true, true);
            return PropertySelector.SelectFormattedValue(propertyName, apiResource, isRawValue, isWithUnit, true);
        }
    }




}