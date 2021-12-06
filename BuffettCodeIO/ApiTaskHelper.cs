using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Resolver;

namespace BuffettCodeIO
{
    public class ApiTaskHelper
    {
        private readonly PeriodSupportedTierResolver tierResolver;

        public ApiTaskHelper(PeriodSupportedTierResolver tierResolver)
        {
            this.tierResolver = tierResolver;
        }

        public SupportedTier FindAvailableTier(DataTypeConfig dataType, string ticker, IPeriod period, bool IsOndemandEndpointEnabled, bool isConfigureAwait, bool useCache)
        {
            var tier = tierResolver.Resolve(ticker, period,
                isConfigureAwait, useCache);
            switch (tier)
            {
                case SupportedTier.None:
                    throw new NotSupportedTierException($"there are no supported tier for {dataType}, {ticker},{period}");
                case SupportedTier.FixedTier:
                    return SupportedTier.FixedTier;
                case SupportedTier.OndemandTier:
                    if (IsOndemandEndpointEnabled)
                    {
                        return SupportedTier.OndemandTier;
                    }
                    else
                    {
                        throw new NotSupportedTierException($"fixed tier does not support the resource: {dataType}, {ticker}, {period}");
                    }
                default:
                    throw new NotSupportedTierException($"unknown tier:{tier} for {dataType}, {ticker},{period}");
            }
        }
        public bool ShouldUseOndemandEndpoint(DataTypeConfig dataType, string ticker, IPeriod period, bool IsOndemandEndpointEnabled, bool isConfigureAwait, bool useCache)
        {
            if (dataType == DataTypeConfig.Indicator)
            {
                return false;
            }
            else
            {
                return FindAvailableTier(dataType, ticker, period, IsOndemandEndpointEnabled, isConfigureAwait, useCache).Equals(SupportedTier.OndemandTier);
            }
        }

        public IComparablePeriod FindEndOfOndemandPeriod(DataTypeConfig dataType, string ticker, PeriodRange<IComparablePeriod> periodRange, bool IsOndemandEndpointEnabled, bool isConfigureAwait, bool useCache)
        {
            var fromTier = FindAvailableTier(dataType, ticker, periodRange.From, IsOndemandEndpointEnabled, isConfigureAwait, useCache);
            var toTier = FindAvailableTier(dataType, ticker, periodRange.To, IsOndemandEndpointEnabled, isConfigureAwait, useCache);

            if (fromTier.Equals(SupportedTier.FixedTier))
            {
                // use fixed tier endpoint for all
                return null;
            }
            else if (toTier.Equals(SupportedTier.OndemandTier))
            {
                return periodRange.To;
            }
            else
            {
                // find end of ondemand tier period 
                var period = periodRange.From.Next();
                while (period.CompareTo(periodRange.To) < 0)
                {
                    var tier = FindAvailableTier(dataType, ticker, period.Next(), IsOndemandEndpointEnabled, isConfigureAwait, useCache);
                    if (tier.Equals(SupportedTier.FixedTier))
                    {
                        break;
                    }
                    else
                    {
                        period = period.Next();
                    }
                }
                return period;
            }
        }
    }
}