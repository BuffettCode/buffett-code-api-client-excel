using BuffettCodeAPIClient.Config;
using BuffettCodeCommon.Validator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BuffettCodeAPIClient
{
    public class BuffettCodeApiV3Client : IBuffettCodeApiClient
    {
        private readonly ApiClientCore httpClient;

        public BuffettCodeApiV3Client(string apiKey)
        {
            this.httpClient = ApiClientCore.Create(apiKey, BuffettCodeApiV3Config.BASE_URL);
        }

        public async Task<String> GetDaily(string ticker, DateTime day, bool useOndemand, bool isConfigureAwait)
        {
            var (endPoint, paramaters) = BuffettCodeApiV3RequestCreator.CreateGetDailyRequest(ticker, day, useOndemand);
            JpTickerValidator.Validate(ticker);
            return await httpClient.Get(endPoint, paramaters, isConfigureAwait);
        }

    }
}