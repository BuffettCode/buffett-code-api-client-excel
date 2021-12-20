using BuffettCodeCommon.Period;
using System;

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

        public uint FixedTierRengeLength() => (uint)Math.Abs(fixedTierRange.TotalGap());

        public uint OndemandTierRengeLength() => (uint)Math.Abs(ondemandTierRange.TotalGap());

    }
}