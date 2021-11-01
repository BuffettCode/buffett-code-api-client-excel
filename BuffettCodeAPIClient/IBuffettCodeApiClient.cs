using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using Newtonsoft.Json.Linq;


namespace BuffettCodeAPIClient
{
    public interface IBuffettCodeApiClient
    {
        JObject Get(DataTypeConfig dataType, string ticker, IPeriod period, bool useOndemand, bool isConfigureAwait, bool useCache);
        JObject GetRange(DataTypeConfig dataType, string ticker, IPeriod from, IPeriod to, bool useOndemand, bool isConfigureAwait, bool useCache);
        string GetApiKey();
        void UpdateApiKey(string apiKey);

    }
}