using System;
using System.Collections.Generic;


namespace BuffettCodeCommon.Period
{
    public interface IPeriod
    {
    }
    public interface IComparablePeriod : IPeriod, IComparable<IPeriod>
    {
        IComparablePeriod Next();
    }
    public interface IApiV2Parameter
    {
        Dictionary<string, string> ToV2Parameter();
    }

    public interface IApiV3Parameter
    {
        Dictionary<string, string> ToV3Parameter();
    }

    public interface IQuarterlyPeriod : IPeriod, IApiV2Parameter, IApiV3Parameter { }
    public interface IDailyPeriod : IPeriod, IApiV3Parameter { }
}