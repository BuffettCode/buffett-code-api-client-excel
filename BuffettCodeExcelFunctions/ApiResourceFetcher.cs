using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO;
using BuffettCodeIO.Property;

namespace BuffettCodeExcelFunctions
{
    public class ApiResourceFetcher
    {

        private static readonly Configuration config = Configuration.GetInstance();
        private readonly BuffettCodeApiTaskProcessor processor;

        public ApiResourceFetcher(BuffettCodeApiVersion version)
        {
            processor = new BuffettCodeApiTaskProcessor(version, config.ApiKey, config.IsOndemandEndpointEnabled
            );
        }

        public IApiResource Fetch(DataTypeConfig dataType, string ticker, IPeriod period, bool isConfigureAwait, bool useCache) => processor.UpdateIfNeeded(config.ApiKey, config.IsOndemandEndpointEnabled).GetApiResource(dataType, ticker, period, isConfigureAwait, useCache);

    }
}