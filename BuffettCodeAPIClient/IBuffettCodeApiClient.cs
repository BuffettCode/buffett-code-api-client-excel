using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace BuffettCodeAPIClient
{
    public interface IBuffettCodeApiClient
    {
        Task<JObject> Get(DataTypeConfig dataType, string ticker, IPeriod period, bool useOndemand, bool isConfigureAwait, bool useCache);
        Task<JObject> GetRange(DataTypeConfig dataType, string ticker, IPeriod from, IPeriod to, bool useOndemand, bool isConfigureAwait, bool useCache);
        string GetApiKey();
        void UpdateApiKey(string apiKey);

    }
}