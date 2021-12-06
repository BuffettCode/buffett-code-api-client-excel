using BuffettCodeCommon.Period;
using System;

namespace BuffettCodeIO.Property
{
    public class FixedTierRange
    {
        private readonly FiscalQuarterPeriod oldestQuarter;
        private readonly FiscalQuarterPeriod latestQuarter;
        private readonly DayPeriod oldestDate;

        public FixedTierRange(FiscalQuarterPeriod oldestQuarter, FiscalQuarterPeriod latestQuarter, DayPeriod oldestDate
            )
        {
            this.oldestQuarter = oldestQuarter;
            this.latestQuarter = latestQuarter;
            this.oldestDate = oldestDate;
        }

        public FiscalQuarterPeriod OldestQuarter => oldestQuarter;
        public FiscalQuarterPeriod LatestQuarter => latestQuarter;
        public DayPeriod OldestDate => oldestDate;

        // currently, latest date is always "today"
        public DayPeriod LatestDate => DayPeriod.Create(DateTime.Today);

    }
}