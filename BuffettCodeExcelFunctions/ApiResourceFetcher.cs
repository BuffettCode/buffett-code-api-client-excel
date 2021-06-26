using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO;
using BuffettCodeIO.Property;
using BuffettCodeIO.Resolver;
namespace BuffettCodeExcelFunctions
{
    public class ApiResourceFetcher
    {
        private static readonly Configuration config = Configuration.GetInstance();
        private static readonly IAPIResolver resolver = APIResolverFactory.Create();
        private static readonly BuffettCodeApiTaskProcessor processor = new BuffettCodeApiTaskProcessor(config.ApiVersion, config.ApiKey, config.MaxDegreeOfParallelism
            );

        public static IApiResource FetchForLegacy
            (string ticker, string parameter1, string parameter2, string propertyName)
        {
            var dataType = resolver.Resolve(propertyName);
            // update processor at first
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    var period = FiscalQuarterPeriod.Create(
                        parameter1, parameter2);
                    return Fetch(dataType, ticker, period);
                case DataTypeConfig.Indicator:
                    return Fetch(dataType, ticker, Snapshot.GetInstance());
                default:
                    throw new NotSupportedDataTypeException();
            }
        }
        public static IApiResource Fetch(DataTypeConfig dataType, string ticker, IPeriod period) => processor.UpdateIfNeeded(config.ApiKey, config.MaxDegreeOfParallelism).GetApiResource(dataType, ticker, period);

    }
}