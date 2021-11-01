using BuffettCodeCommon.Exception;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BuffettCodeAPIClient
{
    /// <summary>
    /// BuffettCode API と Http でやり取りする Client のコアクラス
    /// </summary>

    public class ApiClientCore
    {
        public string ApiKey { set; get; }
        private readonly Uri baseUri;
        private static readonly long TimeoutMilliseconds = 5000;
        private readonly HttpClient httpClient;
        public ApiClientCore(string apiKey, Uri baseUri)
        {
            this.ApiKey = apiKey;
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
            httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            return httpClient;
        }

        public static string BuildGetPath(ApiGetRequest request)
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
                    switch ((int)response.StatusCode)
                    {
                        case (int)HttpStatusCode.Forbidden:
                            throw new InvalidAPIKeyException();
                        case 429: // Quota Error
                            throw new QuotaException();
                        default:
                            throw new BuffettCodeApiClientException();
                    }
                }
                // to do : waiting too long to read as str in csv download
                return await response.Content.ReadAsStringAsync().ConfigureAwait(isConfigureAwait);
            }
        }
    }

}