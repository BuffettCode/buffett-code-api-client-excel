using BuffettCodeAPIClient;
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
        private bool isOndemandEndpointEnabled;
        private static readonly object updateLock = new object();

        public BuffettCodeApiTaskProcessor(BuffettCodeApiVersion version, string apiKey, bool useOndemandEndpoint)
        {
            client = ApiClientFactory.Create(version, apiKey);
            parser = ApiResponseParserFactory.Create(version);
            var tierResolver = PeriodSupportedTierResolver.Create(client, parser);
            taskHelper = new ApiTaskHelper(tierResolver);
            this.isOndemandEndpointEnabled = useOndemandEndpoint;
        }

        public BuffettCodeApiTaskProcessor UpdateIfNeeded(string apiKey, bool useOndemandEndpoint)
        {
            lock (updateLock)
            {
                if (!client.GetApiKey().Equals(apiKey))
                {
                    client.UpdateApiKey(apiKey);
                }
                if (isOndemandEndpointEnabled != useOndemandEndpoint)
                {
                    isOndemandEndpointEnabled = useOndemandEndpoint;
                }
            }
            return this;
        }

        public IApiResource GetApiResource(DataTypeConfig dataType, string ticker, IPeriod period, bool isConfigureAwait = true, bool useCache = true)
        {
            var useOndemand = taskHelper.ShouldUseOndemandEndpoint(dataType, ticker, period, isOndemandEndpointEnabled, isConfigureAwait, useCache);
            var json = client.Get(dataType, ticker, period, useOndemand, isConfigureAwait, useCache);
            return parser.Parse(dataType, json);
        }

        public IList<IApiResource> GetApiResources(DataTypeConfig dataType, string ticker, IComparablePeriod from, IComparablePeriod to, bool isConfigureAwait = true, bool useCache = true)
        {
            if (from.CompareTo(to) > 0)
            {
                throw new ArgumentException($"from={from} is more than to={to}");
            }
            var endOfOndemandPeriod = taskHelper.FindEndOfOndemandPeriod(dataType, ticker, PeriodRange<IComparablePeriod>.Create(from, to), isOndemandEndpointEnabled, isConfigureAwait, useCache);

            // use fixed tier from all range
            if (endOfOndemandPeriod is null)
            {
                var json = client.GetRange(dataType, ticker, from, to, false, isConfigureAwait, useCache);
                return parser.ParseRange(dataType, json);
            }
            else
            {
                var ondemandTierJson = client.GetRange(dataType, ticker, from, endOfOndemandPeriod, true, isConfigureAwait, useCache);
                var fixedTierJson = client.GetRange(dataType, ticker, endOfOndemandPeriod.Next(), to, false, isConfigureAwait, useCache);

                var ondemandResources = parser.ParseRange(dataType, ondemandTierJson);
                var fixedTierResource = parser.ParseRange(dataType, fixedTierJson);

                return ondemandResources.Concat(fixedTierResource).ToList();
            }
        }
    }
}