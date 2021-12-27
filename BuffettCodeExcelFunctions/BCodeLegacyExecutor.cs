using BuffettCodeCommon.Config;
using BuffettCodeIO.Resolver;

namespace BuffettCodeExcelFunctions
{
    public class BCodeLegacyExecutor
    {

        private readonly ILegacyDataTypeResolver resolver;
        private readonly ApiResourceFetcher fetcher;

        public BCodeLegacyExecutor(BuffettCodeApiVersion version)
        {
            resolver = LegacyDataTypeResolver.GetInstance(version);
            fetcher = new ApiResourceFetcher(version);
        }

        public string Execute(string ticker, string fyParameter, string fqParameter, string propertyName, bool isRawValue = false, bool isWithUnit = false)
        {
            var dataType = resolver.Resolve(propertyName);
            var parameter = new TickerParameterBuilderForLegacy().SetDataType(dataType).SetTicker(ticker).SetParameters(fyParameter, fqParameter).Build();
            var apiResource = fetcher.Fetch(dataType, parameter, true, true);
            return PropertySelector.SelectFormattedValue(propertyName, apiResource, isRawValue, isWithUnit, false);
        }
    }
}