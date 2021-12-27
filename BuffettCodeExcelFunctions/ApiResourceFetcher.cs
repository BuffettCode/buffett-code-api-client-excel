using BuffettCodeAPIClient;
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

        public IApiResource Fetch(DataTypeConfig dataType, ITickerPeriodParameter parameter, bool isConfigureAwait, bool useCache) => processor.UpdateIfNeeded(config.ApiKey, config.IsOndemandEndpointEnabled).GetApiResource(dataType, parameter, isConfigureAwait, useCache);

        public FiscalQuarterPeriod FetchLatestFiscalQuarter(string ticker, bool isConfigureAwait, bool useCache)
        {
            var company = Fetch(DataTypeConfig.Company, TickerEmptyPeriodParameter.Create(ticker, Snapshot.GetInstance()), isConfigureAwait, useCache) as Company;
            // use ondemand latest quarter period
            return company.SupportedQuarterRanges.OndemandTierRange.To;
        }

    }
}