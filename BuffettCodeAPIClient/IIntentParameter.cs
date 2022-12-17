using BuffettCodeCommon.Period;
using System.Collections.Generic;

namespace BuffettCodeAPIClient
{

    public interface IIntentParameter
    {
        IIntent GetIntent();
    }

    public interface ITickerParameter
    {
        string GetTicker();

    }

    public interface ITickerIntentParameter : IIntentParameter, ITickerParameter { }

    public interface IApiV2Parameter
    {
        Dictionary<string, string> ToApiV2Parameters();
    }

    public interface IApiV3Parameter
    {
        Dictionary<string, string> ToApiV3Parameters();
    }

}