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

        public string Execute(string ticker, string fyParameter, string fqParameter, string propertyName, bool isRawValue = false, bool isPostfixUnit = false)
        {
            var dataType = resolver.Resolve(propertyName);
            var period = new PeriodBuilderForLegacy().SetDataType(dataType).SetParameters(fyParameter, fqParameter).Build();
            var apiResource = fetcher.Fetch(dataType, ticker, period, true, true);
            return PropertySelector.SelectFormattedValue(propertyName, apiResource, isRawValue, isPostfixUnit);
        }
    }
}