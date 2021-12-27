using BuffettCodeCommon.Config;
using Newtonsoft.Json.Linq;


namespace BuffettCodeAPIClient
{
    public interface IBuffettCodeApiClient
    {
        JObject Get(DataTypeConfig dataType, ITickerPeriodParameter parameter, bool useOndemand, bool isConfigureAwait, bool useCache);
        JObject GetRange(DataTypeConfig dataType, TickerPeriodRangeParameter parameter, bool useOndemand, bool isConfigureAwait, bool useCache);
        string GetApiKey();
        void UpdateApiKey(string apiKey);

    }
}