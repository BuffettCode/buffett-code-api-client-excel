using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using System.Collections.Generic;

namespace BuffettCodeIO.Resolver
{
    public class SupportedTierDictionary
    {
        private readonly Dictionary<string, (SupportedTierRange<FiscalQuarterPeriod>, SupportedTierRange<DayPeriod>)> tickers = new Dictionary<string, (SupportedTierRange<FiscalQuarterPeriod>, SupportedTierRange<DayPeriod>)>();


        public void Add(Company company)
       =>
            tickers.Add(company.Ticker, (company.SupportedQuarterRanges, company.SupportedDailyRanges));

        public bool Has(string ticker) => tickers.ContainsKey(ticker);

        public SupportedTier Get(string ticker, IPeriod period)
        {
            if (!Has(ticker))
            {
                throw new KeyNotFoundException($"ticker={ticker} is not contained.");
            }
            // snapshot is always fixed tier
            else if (period is Snapshot)
            {
                return SupportedTier.FixedTier;
            }
            // latest day is always fixed tier
            else if (period is LatestDayPeriod)
            {
                return SupportedTier.FixedTier;
            }
            // latest fiscal quarter is always fixed tier
            else if (period is LatestFiscalQuarterPeriod)
            {
                return SupportedTier.FixedTier;
            }
            // handle fiscal quarter
            else if (period is FiscalQuarterPeriod fyFq)
            {
                var supportedTiers = tickers[ticker].Item1;
                if (supportedTiers.FixedTierRange.Includes(fyFq))
                {
                    return SupportedTier.FixedTier;
                }
                else if (supportedTiers.OndemandTierRange.Includes(fyFq))
                {
                    return SupportedTier.OndemandTier;
                }
                else
                {
                    return SupportedTier.None;
                }
            }
            // handle day
            else if (period is DayPeriod day)
            {
                var supportedTiers = tickers[ticker].Item2;
                if (supportedTiers.FixedTierRange.Includes(day))
                {
                    return SupportedTier.FixedTier;
                }
                else if (supportedTiers.OndemandTierRange.Includes(day))
                {
                    return SupportedTier.OndemandTier;
                }
                else
                {
                    return SupportedTier.None;
                }
            }
            else
            {
                throw new NotSupportedTierException($"ticker={ticker}, period={period} is not supported.");
            }
        }

    }
}