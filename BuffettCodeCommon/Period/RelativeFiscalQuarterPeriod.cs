using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using System;
using System.Text.RegularExpressions;

namespace BuffettCodeCommon.Period
{
    public class RelativeFiscalQuarterPeriod : IQuarterlyPeriod, IComparablePeriod
    {
        private readonly uint prevYears;
        private readonly uint prevQuarters;

        private RelativeFiscalQuarterPeriod(uint prevYears, uint prevQuarters)
        {
            this.prevYears = prevYears;
            this.prevQuarters = prevQuarters;
        }

        public static RelativeFiscalQuarterPeriod Create(uint prevYears, uint prevQuarters) => new RelativeFiscalQuarterPeriod(prevYears, prevQuarters);

        public static RelativeFiscalQuarterPeriod CreateLatest() => new RelativeFiscalQuarterPeriod(0, 0);

        public static uint ParseRelativeValue(string groupName, Match match)
        {
            var prevStr = match.Groups[groupName].Value.Trim();
            return string.IsNullOrEmpty(prevStr) ? 0 : uint.Parse(prevStr);
        }

        public static RelativeFiscalQuarterPeriod Parse(string relativeExpression)
        {
            var match = PeriodRegularExpressionConfig.RelativeFiscalQuarterRegex
                .Match(relativeExpression);
            if (match.Success)
            {
                var pervYears = ParseRelativeValue("years", match);
                var prevQuarters = ParseRelativeValue("quarters", match);
                return new RelativeFiscalQuarterPeriod(pervYears, prevQuarters);
            }
            else
            {
                throw new ValidationError($"Input {relativeExpression} is not supported pattern");
            }
        }


        public string FiscalYearAsString()
        {
            if (prevYears.Equals(0))
            {
                return ApiRequestParamConfig.ValueLy;
            }
            else
            {
                return $"{ApiRequestParamConfig.ValueLy}-{prevYears}";
            }
        }
        public string FiscalQuarterAsString()
        {
            if (prevQuarters.Equals(0))
            {
                return ApiRequestParamConfig.ValueLq;
            }
            else
            {
                return $"{ApiRequestParamConfig.ValueLq}-{prevQuarters}";
            }
        }


        public uint TotalPrevQuarters => 4 * prevYears + prevQuarters;

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
                var rfq = other as RelativeFiscalQuarterPeriod;
                return (int)TotalPrevQuarters - (int)rfq.TotalPrevQuarters;
            }

        }

        public IComparablePeriod Next()
        {
            if (prevYears.Equals(0) & prevQuarters.Equals(0))
            {
                return null;
            }
            return new RelativeFiscalQuarterPeriod(prevYears, prevQuarters - 1);
        }

        public override string ToString() => $"{FiscalYearAsString()}{FiscalQuarterAsString()}";

        public override int GetHashCode() => (prevYears, prevQuarters).GetHashCode();

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
                var p = obj as RelativeFiscalQuarterPeriod;
                return (this.prevYears == p.prevYears
                    ) && (this.prevQuarters == p.prevQuarters);
            }
        }

        public uint PrevYears => prevYears;
        public uint PrevQuarters => prevQuarters;

    }
}