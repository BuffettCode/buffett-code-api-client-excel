using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient
{
    /// <summary>
    /// BuffettCode API と Http でやり取りする Client のコアクラス
    /// </summary>

    public class ApiClientCore : IDisposable, IApiClientCore
    {
        private string apiKey;
        private readonly Uri baseUri;
        private static readonly long TimeoutMilliseconds = 5000;
        private readonly HttpClient httpClient;
        public ApiClientCore(string apiKey, Uri baseUri)
        {
            this.apiKey = apiKey;
            this.baseUri = baseUri;
            this.httpClient = NewHttpClient();
        }

        private HttpClient NewHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = baseUri;
            httpClient.Timeout = TimeSpan.FromMilliseconds(TimeoutMilliseconds);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
            return httpClient;
        }

        private static string BuildGetPath(ApiGetRequest request)
        {
            return $"{request.EndPoint}?{new FormUrlEncodedContent(request.Parameters).ReadAsStringAsync().Result}";
        }

        public async Task<string> Get(ApiGetRequest request, bool isConfigureAwait)
        {
            var path = BuildGetPath(request);
            using (var response = await httpClient.GetAsync(path).ConfigureAwait(isConfigureAwait))
            {
                if (!response.IsSuccessStatusCode)
                {
                    GetRequestErrorHandler.Handle(request, response);
                }
                var content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public IApiClientCore SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
            return this;
        }

        public string GetApiKey() => this.apiKey;
    }

}