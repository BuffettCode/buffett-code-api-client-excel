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
        private readonly SupportedTierDictionary<FiscalQuarterPeriod> quarterDict;
        private readonly SupportedTierDictionary<DayPeriod> dailyDict;
        private readonly IBuffettCodeApiClient apiClient;
        private readonly IApiResponseParser parser;

        // for unit test
        public PeriodSupportedTierResolver(IBuffettCodeApiClient apiClient, IApiResponseParser parser, SupportedTierDictionary<FiscalQuarterPeriod> quarterDict, SupportedTierDictionary<DayPeriod> dailyDict)
        {
            this.apiClient = apiClient;
            this.parser = parser;
            this.quarterDict = quarterDict;
            this.dailyDict = dailyDict;
        }

        public static PeriodSupportedTierResolver Create(IBuffettCodeApiClient apiClient, IApiResponseParser parser) => new PeriodSupportedTierResolver(apiClient, parser, new SupportedTierDictionary<FiscalQuarterPeriod>(), new SupportedTierDictionary<DayPeriod>());


        public SupportedTier Resolve(string ticker, IPeriod period, bool isConfigureAwait, bool useCache)
        {
            // snapshot is always fixed tier
            if (period is Snapshot)
            {
                return SupportedTier.FixedTier;
            }
            // latest day is always fixed tier
            else if (period is LatestDayPeriod)
            {
                return SupportedTier.FixedTier;
            }
            // handle fiscal quarter
            else if (period is FiscalQuarterPeriod fqp)
            {
                return ResolveQuarterPeriod(ticker, fqp, isConfigureAwait, useCache);
            }
            else if (period is RelativeFiscalQuarterPeriod rfqp)
            {
                return ResolveQuarterPeriod(ticker, rfqp, isConfigureAwait, useCache);
            }
            //  handle day
            else if (period is DayPeriod day)
            {
                if (!dailyDict.Has(ticker))
                {
                    var company = GetCompany(ticker, isConfigureAwait, useCache);
                    dailyDict.Add(company.Ticker, company.SupportedDailyRanges);
                }
                return dailyDict.Get(ticker, day);
            }
            else
            {
                throw new NotSupportedTierException($"period={period} is not supported");
            }

        }

        private SupportedTier ResolveQuarterPeriod(string ticker, RelativeFiscalQuarterPeriod period, bool isConfigureAwait, bool useCache)
        {
            if (!quarterDict.Has(ticker))
            {
                var company = GetCompany(ticker, isConfigureAwait, useCache);
                quarterDict.Add(company.Ticker, company.SupportedQuarterRanges);
            }

            if (period.TotalPrevQuarters <= quarterDict.FixedTierRengeLength(ticker))
            {
                return SupportedTier.FixedTier;
            }
            else if (period.TotalPrevQuarters <= quarterDict.OndemandTierRengeLength(ticker))
            {
                return SupportedTier.OndemandTier;
            }
            else
            {
                return SupportedTier.None;
            }
        }

        private SupportedTier ResolveQuarterPeriod(string ticker, FiscalQuarterPeriod period, bool isConfigureAwait, bool useCache)
        {
            if (!quarterDict.Has(ticker))
            {
                var company = GetCompany(ticker, isConfigureAwait, useCache);
                quarterDict.Add(company.Ticker, company.SupportedQuarterRanges);
            }
            return quarterDict.Get(ticker, period);
        }

        private Company GetCompany(string ticker, bool isConfigureAwait, bool useCache)
        {
            var json = apiClient.Get(DataTypeConfig.Company, TickerEmptyPeriodParameter.Create(ticker, Snapshot.GetInstance()), false, isConfigureAwait, useCache);
            return parser.Parse(DataTypeConfig.Company, json) as Company;
        }
    }
}