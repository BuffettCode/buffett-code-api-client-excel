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

        public static IEnumerable<PeriodRange<T>> Slice(PeriodRange<T> range, uint size)
        {
            if (size == 0)
            {
                throw new ArgumentException("size must be positive number.");
            }
            uint totalGap = (uint)ComparablePeriodUtil.GetGap(range.From, range.To);
            var cursor = range.From;
            var start = cursor;

            for (uint num = 1; num <= totalGap; num++)
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
    }


}