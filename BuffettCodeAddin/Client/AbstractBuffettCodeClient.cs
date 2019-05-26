using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BuffettCodeAddin.Client
{
    public abstract class AbstractBuffettCodeClient : IBuffettCodeClient
    {
        abstract public Task<string> GetIndicator(string apiKey, string ticker, bool isConfigureAwait = true);

        abstract public Task<string> GetQuarter(string apiKey, string ticker, string fiscalYear, string fiscalQuarter, bool isConfigureAwait = true);

        abstract public Task<string> GetQuarterRange(string apiKey, string ticker, string from, string to, bool isConfigureAwait = true);

        protected async Task<string> Request(string apiKey, string path, bool isConfigureAwait)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.buffett-code.com/");
                client.Timeout = TimeSpan.FromMilliseconds(5000);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);

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
                            throw new BuffettCodeException();
                    }
                }
                return await response.Content.ReadAsStringAsync().ConfigureAwait(isConfigureAwait);
            }
        }

        protected static string BuildGetPath(string path, Dictionary<string, string> parameters)
        {
            return $"{path}?{new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result}";
        }

        protected static string ToQuarter(string fiscalYear, string fiscalQuarter)
        {
            return fiscalYear + "Q" + fiscalQuarter;
        }

        protected static string ToForwardQuarter(string fiscalYear, string fiscalQuarter, int n)
        {
            int fy = Int32.Parse(fiscalYear);
            int fq = Int32.Parse(fiscalQuarter);

            int nfy = fy + (fq + n - 1) / 4;
            int nfq = (fq + n - 1) % 4 + 1;
            return ToQuarter(nfy.ToString(), nfq.ToString());
        }

        protected static string ToLowerLimitQuarter(string fiscalYear, string fiscalQuarter)
        {
            int fy = Int32.Parse(fiscalYear) - 3;
            return ToForwardQuarter(fy.ToString(), fiscalQuarter, 2);
        }

        protected static string ToUpperLimitQuarter(string fiscalYear, string fiscalQuarter)
        {
            return ToForwardQuarter(fiscalYear, fiscalQuarter, 1);
        }
    }
}
