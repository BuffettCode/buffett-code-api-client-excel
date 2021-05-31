using BuffettCodeCommon.Validator;
using System;

namespace BuffettCodeCommon.Period
{
    public class FiscalQuarterPeriod : IPeriod, IComparable<FiscalQuarterPeriod>
    {
        private readonly uint year;
        private readonly uint quarter;

        private FiscalQuarterPeriod(uint fiscalYear, uint fiscalQuarter)
        {
            year = fiscalYear;
            quarter = fiscalQuarter;
        }

        public uint Year => year;
        public uint Quarter => quarter;

        public static FiscalQuarterPeriod Create(uint fiscalYear, uint fiscalQuarter)
        {
            FiscalYearValidator.Validate(fiscalYear);
            FiscalQuarterValidator.Validate(fiscalQuarter);
            return new FiscalQuarterPeriod(fiscalYear, fiscalQuarter);
        }

        public int CompareTo(FiscalQuarterPeriod other)
        {
            if (other is null)
            {
                throw new ArgumentNullException();
            }
            else if (year != other.year)
            {
                return year < other.year ? -1 : 1;
            }
            else if (quarter != other.quarter)
            {
                return quarter < other.quarter ? -1 : 1;
            }
            else
            {
                return 0;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (this.GetType() != obj.GetType())
            {
                return false;
            }
            else if (this.GetHashCode() != obj.GetHashCode())
            {
                return false;
            }
            else
            {
                var p = (FiscalQuarterPeriod)obj;
                return (this.year == p.year
                    ) && (this.quarter == p.quarter);
            }
        }

        public override int GetHashCode() => (year, quarter).GetHashCode();

        public override string ToString() => $"{year}Q{quarter}";
    }
}