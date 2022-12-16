using System;


namespace BuffettCodeCommon.Period
{
    public interface IIntent
    {
    }
    public interface IComparablePeriod : IIntent, IComparable<IIntent>
    {
        IComparablePeriod Next();
    }
    public interface IQuarterlyPeriod : IIntent { }
    public interface IDailyPeriod : IIntent { }
}