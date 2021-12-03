using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using System;
using System.Collections.Generic;

namespace BuffettCodeCommon.Period
{
    public class PeriodRange<T> where T : IComparablePeriod
    {
        private readonly T from;
        private readonly T to;

        public T From => from;
        public T To => to;

        private PeriodRange(T from, T to)
        {
            this.from = from;
            this.to = to;
        }

        public static PeriodRange<T> Create(T from, T to)
        {
            if (from.GetType() != to.GetType())
            {
                throw new ArgumentException($"from and to must be same types, but from:{from.GetType()} and {to.GetType()}");
            }
            else if (from.CompareTo(to) > 0)
            {
                throw new ValidationError($"to must be more than equal from, but from:{from} and {to} given.");
            }
            else
            {
                return new PeriodRange<T>(from, to);
            }
        }

        public bool Includes(T period)
        {
            if (period.CompareTo(From) < 0)
            {
                return false;
            }
            else if (period.CompareTo(To) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static IEnumerable<PeriodRange<T>> Slice(PeriodRange<T> range, uint size)
        {
            if (size == 0)
            {
                throw new ArgumentException("size must be positive number.");
            }
            var totalGap = ComparablePeriodUtil.GetGap(range.From, range.To);
            if (totalGap < 0)
            {
                throw new ArgumentOutOfRangeException($"given range is broken, from={range.From}, to={range.To}");
            }

            var cursor = range.From;
            var start = cursor;

            for (uint num = 1; num <= (uint)totalGap; num++)
            {
                if (num % size == 0)
                {
                    yield return Create(start, cursor);
                    cursor = (T)cursor.Next();
                    start = cursor;
                }
                else
                {
                    cursor = (T)cursor.Next();
                }
            }
            // the last
            if (start.CompareTo(range.To) <= 0)
            {
                yield return Create(start, range.To);
            }
        }

        private Dictionary<string, string> ToApiParameter() => new Dictionary<string, string>() { { ApiRequestParamConfig.KeyFrom, from
        .ToString() }, { ApiRequestParamConfig.KeyTo, to.ToString() } };

    }

}