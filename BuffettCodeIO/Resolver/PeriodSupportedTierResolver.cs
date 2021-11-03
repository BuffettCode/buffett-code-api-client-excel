using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Parser;
using BuffettCodeIO.Property;

namespace BuffettCodeIO.Resolver
{
    public class PeriodSupportedTierResolver
    {
        private readonly SupportedTierDictionary supportedTierDict;
        private readonly IBuffettCodeApiClient apiClient;
        private readonly IApiResponseParser parser;

        // for unit test
        public PeriodSupportedTierResolver(IBuffettCodeApiClient apiClient, IApiResponseParser parser, SupportedTierDictionary supportedTierDict)
        {
            this.apiClient = apiClient;
            this.parser = parser;
            this.supportedTierDict = supportedTierDict;
        }

        public static PeriodSupportedTierResolver Create(IBuffettCodeApiClient apiClient, IApiResponseParser parser) => new PeriodSupportedTierResolver(apiClient, parser, new SupportedTierDictionary());

        public SupportedTier Resolve(DataTypeConfig dataType, string ticker, IPeriod period, bool isConfigureAwait, bool useCache)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return ResolveQuarter(ticker, period as FiscalQuarterPeriod, isConfigureAwait, useCache);
                default:
                    throw new NotSupportedDataTypeException($"dataType={dataType} is not supported.");
            }
        }

        private Company GetCompany(string ticker, bool isConfigureAwait, bool useCache)
        {
            var json = apiClient.Get(DataTypeConfig.Company, ticker, Snapshot.GetInstance(), false, isConfigureAwait, useCache);
            return parser.Parse(DataTypeConfig.Company, json) as Company;
        }

        private SupportedTier ResolveQuarter(string ticker, FiscalQuarterPeriod period, bool isConfigureAwait, bool useCache)
        {
            if (!supportedTierDict.Has(ticker, DataTypeConfig.Quarter))
            {
                var company = GetCompany(ticker, isConfigureAwait, useCache);
                supportedTierDict.Add(company);
            }
            return supportedTierDict.Get(ticker, period);
        }
    }
}