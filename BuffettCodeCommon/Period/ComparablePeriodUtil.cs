using System;

namespace BuffettCodeCommon.Period
{
    public class ComparablePeriodUtil
    {
        public static int GetGap(IComparablePeriod start, IComparablePeriod end)
        {
            if (start is FiscalQuarterPeriod sfq && end is FiscalQuarterPeriod efq)
            {
                return GetGap(sfq, efq);
            }
            else if (start is DayPeriod sd && end is DayPeriod ed)
            {
                return GetGap(sd, ed);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static int GetGap(FiscalQuarterPeriod start, FiscalQuarterPeriod end) => (int)(end.Year - start.Year) * 4 + (int)(end.Quarter - start.Quarter);

        public static int GetGap(DayPeriod start, DayPeriod end) => (int)end.Value.Subtract(start.Value).TotalDays;

    }
}
