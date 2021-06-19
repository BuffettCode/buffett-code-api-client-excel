using System;
namespace BuffettCodeCommon.Period
{
    public class Day : IPeriod, IComparable<Day>
    {
        private readonly DateTime value;
        private Day(DateTime date)
        {
            value = date.Date;
        }

        public DateTime Value => value;

        public static Day Create(uint year, uint month, uint day)
        {
            return new Day(new DateTime((int)year, (int)month, (int)day));
        }

        public static Day Create(DateTime date)
        {
            return new Day(date);
        }

        public override int GetHashCode() => value.GetHashCode();

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
                var d = (Day)obj;
                return this.value.Year == d.value.Year
                    && this.value.Month == d.value.Month
                    && this.value.Day == d.value.Month;
            }
        }
        public override string ToString() => value.ToShortDateString();

        public int CompareTo(Day other) => value.CompareTo(other.value);
    }

}