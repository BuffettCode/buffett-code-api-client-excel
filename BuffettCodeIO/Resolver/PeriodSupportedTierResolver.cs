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

        public SupportedTier Resolve(DataTypeConfig dataType, string ticker, IPeriod period)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return ResolveQuarter(ticker, period as FiscalQuarterPeriod);
                default:
                    throw new NotSupportedDataTypeException($"dataType={dataType} is not supported.");
            }
        }

        private Company GetCompany(string ticker)
        {
            var json = apiClient.Get(DataTypeConfig.Company, ticker, Snapshot.GetInstance(), false, true, true).Result;
            return parser.Parse(DataTypeConfig.Company, json) as Company;
        }

        public SupportedTier ResolveQuarter(string ticker, FiscalQuarterPeriod period)
        {
            if (!supportedTierDict.Has(ticker, DataTypeConfig.Quarter))
            {
                var company = GetCompany(ticker);
                supportedTierDict.Add(company);
            }
            return supportedTierDict.Get(ticker, period);
        }
    }
}