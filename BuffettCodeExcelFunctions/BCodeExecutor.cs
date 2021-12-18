using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;

namespace BuffettCodeExcelFunctions
{
    public class BCodeExecutor
    {
        private readonly ApiResourceFetcher fetcher;

        public BCodeExecutor(BuffettCodeApiVersion version)
        {
            fetcher = new ApiResourceFetcher(version);
        }

        public string Execute(string ticker, DataTypeConfig dataType, IPeriod period, string propertyName, bool isRawValue = false, bool isWithUnit = false)
        {
            var apiResource = fetcher.Fetch(dataType, ticker, period, true, true);
            // todo use default unit config
            return PropertySelector.SelectFormattedValue(propertyName, apiResource, isRawValue, isWithUnit);
        }
    }




}