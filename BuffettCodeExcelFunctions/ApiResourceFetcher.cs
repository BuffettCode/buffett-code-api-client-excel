using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO;
using BuffettCodeIO.Property;
using BuffettCodeIO.Resolver;

namespace BuffettCodeExcelFunctions
{
    public static class ApiResourceFetcher
    {
        private static readonly BuffettCodeApiVersion verion = BuffettCodeApiVersion.Version2;
        private static readonly Configuration config = Configuration.GetInstance();
        private readonly static BuffettCodeApiTaskProcessor processor = new BuffettCodeApiTaskProcessor(verion, config.ApiKey, config.IsOndemandEndpointEnabled
            );


        private static readonly ILegacyDataTypeResolver resolver = LegacyDataTypeResolver.GetInstance(verion);

        public static IApiResource FetchForLegacy
            (string ticker, string parameter1, string parameter2, string propertyName, bool isConfigureAwait, bool useCache)
        {
            var dataType = resolver.Resolve(propertyName);
            var period = new PeriodBuilderForLegacy().SetDataType(dataType).SetParameters(parameter1, parameter2).Build();
            return GetApiResource(dataType, ticker, period, isConfigureAwait, useCache);
        }

        private static IApiResource GetApiResource(DataTypeConfig dataType, string ticker, IPeriod period, bool isConfigureAwait, bool useCache) => processor.UpdateIfNeeded(config.ApiKey, config.IsOndemandEndpointEnabled).GetApiResource(dataType, ticker, period, isConfigureAwait, useCache);

        public static IApiResource Fetch(string ticker, IPeriod period, bool isConfigureAwait, bool useCache)
        {
            // dataType
            var dataType = DataTypeConfig.Quarter;
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return GetApiResource(dataType, ticker, period, isConfigureAwait, useCache);
                case DataTypeConfig.Indicator:
                    return GetApiResource(dataType, ticker, Snapshot.GetInstance(), isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException();
            }
        }

    }
}