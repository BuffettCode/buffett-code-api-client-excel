using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuffettCodeAddin.Client
{
    /// <summary>
    /// バフェットコードクライアント(v1)
    /// </summary>
    [Obsolete("特に理由がなければ最新バージョンのクライアントを使用してください")]
    public class BuffettCodeClientV1 : AbstractBuffettCodeClient
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
            var path = BuildGetPath("/api/v1/quarter", parameters);
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
            var path = BuildGetPath("/api/v1/quarter", parameters);
            return await Request(apiKey, path, isConfigureAwait).ConfigureAwait(isConfigureAwait);
        }

        /// <inheritdoc/>
        public override Task<string> GetIndicator(string apiKey, string ticker, bool isConfigureAwait = true)
        {
            throw new NotImplementedException();
        }
    }
}
