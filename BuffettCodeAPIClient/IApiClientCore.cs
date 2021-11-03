using System.Threading.Tasks;

namespace BuffettCodeAPIClient
{
    public interface IApiClientCore
    {
        Task<string> Get(ApiGetRequest request, bool isConfigureAwait);

        IApiClientCore SetApiKey(string apiKey);
        string GetApiKey();

    }
}