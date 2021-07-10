using BuffettCodeCommon.Period;

namespace BuffettCodeIO.Property
{
    public class SupportedTierRange<T> where T : IComparablePeriod
    {
        private readonly PeriodRange<T> fixedTierRange;
        private readonly PeriodRange<T> ondemandTierRange;

        public SupportedTierRange(
            PeriodRange<T> fixedTierRange, PeriodRange<T> ondemandTierRange)
        {
            this.fixedTierRange = fixedTierRange;
            this.ondemandTierRange = ondemandTierRange;
        }

        public PeriodRange<T> FixedTierRange => fixedTierRange;
        public PeriodRange<T> OndemandTierRange => ondemandTierRange;
    }
}