using BuffettCodeCommon.Validator;
using System;

namespace BuffettCodeCommon.Period
{
    public class FiscalQuarterPeriod : IComparablePeriod
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

        public static FiscalQuarterPeriod Create(string fiscalYear, string fiscalQuarter) => new FiscalQuarterPeriod(uint.Parse(fiscalYear), uint.Parse(fiscalQuarter));
        public static FiscalQuarterPeriod Create(float fiscalYear, float fiscalQuarter) => new FiscalQuarterPeriod((uint)fiscalYear, (uint)fiscalQuarter);

        public static FiscalQuarterPeriod Create(int fiscalYear, int fiscalQuarter) => new FiscalQuarterPeriod((uint)fiscalYear, (uint)fiscalQuarter);

        public static FiscalQuarterPeriod Parse(string fyFqString)
        {
            var fyFq = fyFqString.Split('Q');
            return FiscalQuarterPeriod.Create(fyFq[0], fyFq[1]);
        }

        public int CompareTo(IPeriod other)
        {
            if (other is null)
            {
                throw new ArgumentNullException();
            }
            else if (this.GetType() != other.GetType())
            {
                throw new ArgumentException($"Compare to {other.GetType()} is not supported.");
            }
            else
            {
                var fqp = (FiscalQuarterPeriod)other;
                if (year != fqp.year)
                {
                    return year < fqp.year ? -1 : 1;
                }
                else if (quarter != fqp.quarter)
                {
                    return quarter < fqp.quarter ? -1 : 1;
                }
                else
                {
                    return 0;
                }
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

        public IComparablePeriod Next() => (Quarter < 4) ? Create(Year, Quarter + 1) : Create(Year + 1, 1);

        public override string ToString() => $"{year}Q{quarter}";
    }
}