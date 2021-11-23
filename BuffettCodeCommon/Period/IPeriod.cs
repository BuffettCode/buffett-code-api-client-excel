using System;


namespace BuffettCodeCommon.Period
{
    public interface IPeriod
    {
    }
    public interface IComparablePeriod : IPeriod, IComparable<IPeriod>
    {
        IComparablePeriod Next();
    }

    public interface IQuarterlyPeriod : IPeriod { }
    public interface IDailyPeriod : IPeriod { }
}