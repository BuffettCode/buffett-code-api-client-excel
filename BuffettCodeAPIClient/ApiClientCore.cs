using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BuffettCodeCommon.Exception;

namespace BuffettCodeAPIClient
{
    /// <summary>
    /// BuffettCode API と Http でやり取りする Client のコアクラス
    /// </summary>

    public class ApiClientCore
    {
        private readonly string apiKey;
        private readonly Uri baseUri;
        private static long TimeoutMilliseconds = 5000;

        private ApiClientCore(string apiKey, Uri baseUri)
        {
            this.apiKey = apiKey;
            this.baseUri = baseUri;
        }

        public static ApiClientCore Create(string apiKey, string baseUrl)
        {
            return new ApiClientCore(apiKey, new Uri(baseUrl));
        }

        public static ApiClientCore Create(string apiKey, Uri baseUrl)
        {
            return new ApiClientCore(apiKey, baseUrl);
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

        public static string BuildGetPath(string endpoint, Dictionary<string, string> parameters)
        {
            return $"{endpoint}?{new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result}";
        }

        public async Task<string> Get(string endpoint, Dictionary<string, string> parameters, bool isConfigureAwait)
        {
            var path = BuildGetPath(endpoint, parameters);
            using (var client = NewHttpClient())
            {
                var response = await client.GetAsync(path).ConfigureAwait(isConfigureAwait);
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
                return await response.Content.ReadAsStringAsync().ConfigureAwait(isConfigureAwait);
            }
        }
    }


}