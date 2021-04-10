using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV2Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCore httpClient;
        public BuffettCodeApiV2Client(string apiKey)
        {
            this.httpClient = ApiClientCore.Create(apiKey, BuffettCodeApiV2Config.BASE_URL);
        }

        public async Task<String> GetQuarter(string ticker, uint fiscalYear, uint fiscalQuarter, bool useOndemand, bool isConfigureAwait)
        {
            var (endpoint, paramaters) = BuffettCodeApiV2RequestCreator.CreateGetQuarterRequest(ticker, fiscalYear, fiscalQuarter, useOndemand);
            return await httpClient.Get(endpoint, paramaters, isConfigureAwait);
        }

        public async Task<String> GetIndicator(string ticker, bool isConfigureAwait)
        {
            var (endpoint, paramaters) = BuffettCodeApiV2RequestCreator.CreateGetIndicatorRequest
                (ticker);
            return await httpClient.Get(endpoint, paramaters, isConfigureAwait);
        }



    }
}