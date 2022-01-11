using BuffettCodeAPIClient;
using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Parser;
using BuffettCodeIO.Property;
using BuffettCodeIO.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO
{
    public class BuffettCodeApiTaskProcessor
    {
        private readonly IBuffettCodeApiClient client;
        private readonly IApiResponseParser parser;
        private readonly ApiTaskHelper taskHelper;
        private static readonly Configuration config = Configuration.GetInstance();

        public BuffettCodeApiTaskProcessor(BuffettCodeApiVersion version)
        {
            client = ApiClientFactory.Create(version, config.ApiKey);
            parser = ApiResponseParserFactory.Create(version);
            var tierResolver = PeriodSupportedTierResolver.Create(client, parser);
            taskHelper = new ApiTaskHelper(tierResolver);
        }

        private void UpdateApiKeyIfNeeded()
        {
            if (!client.GetApiKey().Equals(config.ApiKey))
            {
                client.UpdateApiKey(config.ApiKey);
            }
        }

        public IApiResource GetApiResource(DataTypeConfig dataType, ITickerPeriodParameter parameter, bool isConfigureAwait = true, bool useCache = true)
        {
            UpdateApiKeyIfNeeded();
            var useOndemand = config.IsForceOndemandApi ? true : taskHelper.ShouldUseOndemandEndpoint(dataType, parameter.GetTicker(), parameter.GetPeriod(), config.IsOndemandEndpointEnabled, isConfigureAwait, useCache);
            client.UpdateApiKey(config.ApiKey);
            var json = client.Get(dataType, parameter, useOndemand, isConfigureAwait, useCache);
            return parser.Parse(dataType, json);
        }

        public IList<IApiResource> GetApiResources(DataTypeConfig dataType, string ticker, IComparablePeriod from, IComparablePeriod to, bool isConfigureAwait = true, bool useCache = true)
        {
            UpdateApiKeyIfNeeded();
            if (from.CompareTo(to) > 0)
            {
                throw new ArgumentException($"from={from} is more than to={to}");
            }
            var endOfOndemandPeriod = taskHelper.FindEndOfOndemandPeriod(dataType, ticker, PeriodRange<IComparablePeriod>.Create(from, to), config.IsOndemandEndpointEnabled, isConfigureAwait, useCache);

            // use fixed tier from all range
            if (endOfOndemandPeriod is null)
            {
                var parameter = TickerPeriodRangeParameter.Create(ticker, from, to);
                var json = client.GetRange(dataType, parameter, false, isConfigureAwait, useCache);
                return parser.ParseRange(dataType, json);
            }
            else
            {

                var ondemandParameter = TickerPeriodRangeParameter.Create(ticker, from, endOfOndemandPeriod);
                var fixedParameter = TickerPeriodRangeParameter.Create(ticker, endOfOndemandPeriod.Next(), to);

                var ondemandTierJson = client.GetRange(dataType, ondemandParameter, true, isConfigureAwait, useCache);
                var fixedTierJson = client.GetRange(dataType, fixedParameter, false, isConfigureAwait, useCache);

                var ondemandResources = parser.ParseRange(dataType, ondemandTierJson);
                var fixedTierResource = parser.ParseRange(dataType, fixedTierJson);

                return ondemandResources.Concat(fixedTierResource).ToList();
            }
        }
    }
}