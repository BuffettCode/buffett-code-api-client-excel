using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeCommon.Validator;
using BuffettCodeIO.Property;
using System.Collections.Generic;

namespace BuffettCodeIO.Resolver
{
    public class SupportedTierDictionary<T> where T : IComparablePeriod
    {
        private readonly Dictionary<string, SupportedTierRange<T>> tickerTierDict
            = new Dictionary<string, SupportedTierRange<T>>();

        public void Add(string ticker, SupportedTierRange<T> supportedTierRange)
        {
            JpTickerValidator.Validate(ticker);
            tickerTierDict.Add(ticker, supportedTierRange);
        }


        public bool Has(string ticker)
        {
            JpTickerValidator.Validate(ticker);
            return tickerTierDict.ContainsKey(ticker);
        }

        public SupportedTier Get(string ticker, T period)
        {
            JpTickerValidator.Validate(ticker);
            if (!Has(ticker))
            {
                throw new KeyNotFoundException($"ticker={ticker} is not contained.");
            }
            else
            {
                var supportedTier = tickerTierDict[ticker];
                if (supportedTier.FixedTierRange.Includes(period))
                {
                    return SupportedTier.FixedTier;
                }
                else if (supportedTier.OndemandTierRange.Includes(period))
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