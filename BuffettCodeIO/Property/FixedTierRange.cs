using BuffettCodeCommon.Period;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeIO.Property
{
    public class FixedTierRange
    {
        private FiscalQuarterPeriod oldestQuarter;
        private FiscalQuarterPeriod latestQuarter;
        private DayPeriod oldestDate;

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