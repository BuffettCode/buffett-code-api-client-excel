using System.Threading.Tasks;

namespace BuffettCodeAPIClient.Tests
{
    class MockApiClientCore : IApiClientCore
    {
        public static readonly string Response = "{\"name\":\"John\", \"age\":30, \"car\":null}";
        private string apiKey;
        public MockApiClientCore(string apiKey)
        {
            this.apiKey = apiKey;
        }



        public Task<string> Get(ApiGetRequest request, bool isConfigureAwait)
        {
            return Task<string>.FromResult(Response);
        }

        public string GetApiKey() => this.apiKey;
        public IApiClientCore SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
            return this;
        }
    }
}