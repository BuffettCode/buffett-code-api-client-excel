using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuffettCodeAPIAdapter.Client
{
    /// <summary>
    /// バフェットコードクライアント(v2)
    /// </summary>
    public class BuffettCodeClientV2 : AbstractBuffettCodeClient
    {
        /// <inheritdoc/>
        public override async Task<string> GetQuarter(string apiKey, string ticker, string fiscalYear, string fiscalQuarter, bool isConfigureAwait = true)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "tickers", ticker },
                { "from", ToLowerLimitQuarter(fiscalYear, fiscalQuarter) },
                { "to", ToUpperLimitQuarter(fiscalYear, fiscalQuarter) },
            };
            var path = BuildGetPath("/api/v2/quarter", parameters);
            return await Request(apiKey, path, isConfigureAwait).ConfigureAwait(isConfigureAwait);
        }

        /// <inheritdoc/>
        public override async Task<string> GetQuarterRange(string apiKey, string ticker, string from, string to, bool isConfigureAwait = true)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "tickers", ticker },
                { "from", from },
                { "to", to },
            };
            var path = BuildGetPath("/api/v2/quarter", parameters);
            return await Request(apiKey, path, isConfigureAwait).ConfigureAwait(isConfigureAwait);
        }

        /// <inheritdoc/>
        public override async Task<string> GetIndicator(string apiKey, string ticker, bool isConfigureAwait = true)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "tickers", ticker },
            };
            var path = BuildGetPath("/api/v2/indicator", parameters);
            return await Request(apiKey, path, isConfigureAwait).ConfigureAwait(isConfigureAwait);
        }
    }
}
