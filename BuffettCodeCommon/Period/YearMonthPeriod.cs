using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuffettCodeCommon.Period
{
    public class YearMonthPeriod : IComparablePeriod, IYearMonthPeriod
    {
        private readonly uint year;
        private readonly uint month;

        private YearMonthPeriod(uint year, uint month)
        {
            this.year = year;
            this.month = month;
        }

        public uint Year => year;
        public uint Month => month;

        public static YearMonthPeriod Create(uint year, uint month)
        {
            FiscalYearValidator.Validate(year);
            MonthValidator.Validate(month);
            return new YearMonthPeriod(year, month);
        }

        public static YearMonthPeriod Parse(string yearMonth)
        {
            var yearStr = yearMonth.Substring(0, 4);
            var monthStr = yearMonth.Substring(4, 2);
            return Create(uint.Parse(yearStr), uint.Parse(monthStr));
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
                var p = (YearMonthPeriod)obj;
                return (this.year == p.year) && (this.month == p.month);
            }
        }

        public override int GetHashCode() => (year, month).GetHashCode();

        public IComparablePeriod Next() => (Month < 12) ? Create(Year, Month + 1) : Create(Year + 1, 1);

        public override string ToString() => String.Format("{0}{1:00}", year, month);


        public int CompareTo(IIntent other)
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
                var ymp = (YearMonthPeriod)other;
                if (year != ymp.year)
                {
                    return year < ymp.year ? -1 : 1;
                }
                else if (month != ymp.month)
                {
                    return month < ymp.month ? -1 : 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}