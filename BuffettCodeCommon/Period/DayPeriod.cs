using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BuffettCodeCommon.Period
{
    public class DayPeriod : IComparablePeriod, IDailyPeriod
    {
        private static readonly string dateFormat = "yyyy-MM-dd";
        private static readonly string dateStrPattern = "^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])";
        private readonly DateTime value;
        private DayPeriod(DateTime date)
        {
            value = date.Date;
        }

        public DateTime Value => value;

        public static DayPeriod Create(uint year, uint month, uint day)
        {
            return new DayPeriod(new DateTime((int)year, (int)month, (int)day));
        }

        public static DayPeriod Create(DateTime date)
        {
            return new DayPeriod(date);
        }

        public static DayPeriod Parse(string input)
        {
            var dateStr = FindDateStr(input);
            var date = DateTime.ParseExact(dateStr,
                                  dateFormat,
                                  CultureInfo.InvariantCulture);
            return new DayPeriod(date);
        }

        private static string FindDateStr(string input)
            => Regex.Match(input, dateStrPattern).Value;

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
                var d = (DayPeriod)obj;
                return this.value.Year == d.value.Year
                    && this.value.Month == d.value.Month
                    && this.value.Day == d.value.Day;
            }
        }
        public override string ToString() => value.ToString(dateFormat);

        public int CompareTo(IPeriod other)
        {
            if (other is null)
            {
                throw new ArgumentNullException();
            }
            else if (this.GetType() != other.GetType())
            {
                throw new ArgumentException($"Compare to{other.GetType()} is not supported.");
            }
            else
            {
                return value.CompareTo(((DayPeriod)other).value);
            }
        }

        public IComparablePeriod Next() => Create(Value.AddDays(1));

    }

}