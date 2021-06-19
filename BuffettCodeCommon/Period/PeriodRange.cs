using BuffettCodeCommon.Exception;
using System;
namespace BuffettCodeCommon.Period
{
    public class PeriodRange<T> where T : IPeriod, IComparable<IPeriod>
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
    }

}