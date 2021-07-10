using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using System.Collections.Generic;

namespace BuffettCodeIO.Resolver
{
    public class SupportedTierDictionary
    {
        private readonly Dictionary<string, SupportedTierRange<FiscalQuarterPeriod>> quarters = new Dictionary<string, SupportedTierRange<FiscalQuarterPeriod>>();

        public void Add(Company company) => quarters.Add(company.Ticker, company.SupportedQuarterRanges);
        public bool Has(string ticker, DataTypeConfig dataType)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return quarters.ContainsKey(ticker);
                default:
                    throw new NotSupportedDataTypeException($"dataType={dataType} is not supported.");
            }

        }

        public SupportedTier Get(string ticker, FiscalQuarterPeriod period)
        {
            if (!Has(ticker, DataTypeConfig.Quarter))
            {
                throw new KeyNotFoundException($"ticker={ticker} is not contained.");
            }
            else
            {
                var supportedTiers = quarters[ticker];
                if (supportedTiers.FixedTierRange.Includes(period))
                {
                    return SupportedTier.FixedTier;
                }
                else if (supportedTiers.OndemandTierRange.Includes(period))
                {
                    return SupportedTier.OndemandTier;
                }
                else
                {
                    return SupportedTier.None;
                }
            }

        }

    }
}