using BuffettCodeCommon.Exception;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient.Tests
{
    class ErrorMockApiClientCore : IApiClientCore
    {
        private string apiKey;
        public ErrorMockApiClientCore(string apiKey)
        {
            this.apiKey = apiKey;
        }


        public Task<string> Get(ApiGetRequest request, bool isConfigureAwait)
        {
            throw new BuffettCodeApiClientException();
        }

        public string GetApiKey() => this.apiKey;
        public IApiClientCore SetApiKey(string apikey)
        {
            apiKey = apikey;
            return this;
        }
    }
}