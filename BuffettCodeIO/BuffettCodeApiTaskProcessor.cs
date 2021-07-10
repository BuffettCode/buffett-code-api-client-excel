using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Parser;
using BuffettCodeIO.Processor;
using BuffettCodeIO.Property;
using BuffettCodeIO.Resolver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO
{
    public class BuffettCodeApiTaskProcessor
    {
        private readonly IBuffettCodeApiClient client;
        private readonly IApiResponseParser parser;
        private readonly ITaskProcessor<JObject> processor;
        private readonly ApiTaskHelper taskHelper;
        private bool isOndemandEndpointEnabled;
        private static readonly object updateLock = new object();

        public BuffettCodeApiTaskProcessor(BuffettCodeApiVersion version, string apiKey, uint maxDegreeOfParallelism, bool useOndemandEndpoint)
        {
            client = ApiClientInstanceGetter.Get(version, apiKey);
            parser = ApiResponseParserFactory.Create(version);
            var tierResolver = PeriodSupportedTierResolver.Create(client, parser);
            taskHelper = new ApiTaskHelper(tierResolver);
            processor = new SemaphoreTaskProcessor<JObject>(maxDegreeOfParallelism);
            this.isOndemandEndpointEnabled = useOndemandEndpoint;
        }

        public BuffettCodeApiTaskProcessor UpdateIfNeeded(string apiKey, uint maxDegreeOfParallelism, bool useOndemandEndpoint)
        {
            lock (updateLock)
            {
                if (!client.GetApiKey().Equals(apiKey))
                {
                    client.UpdateApiKey(apiKey);
                }
                if (!processor.GetMaxDegreeOfParallelism().Equals
                    (maxDegreeOfParallelism))
                {
                    processor.UpdateMaxDegreeOfParallelism(maxDegreeOfParallelism);
                }

                if (this.isOndemandEndpointEnabled != useOndemandEndpoint)
                {
                    this.isOndemandEndpointEnabled = useOndemandEndpoint;
                }
            }
            return this;
        }

        public IApiResource GetApiResource(DataTypeConfig dataType, string ticker, IPeriod period)
        {
            var useOndemand = taskHelper.ShouldUseOndemandEndpoint(dataType, ticker, period, isOndemandEndpointEnabled);
            var json = processor.Process(client.Get(dataType, ticker, period, useOndemand, true, true));
            return parser.Parse(dataType, json);
        }

        public IList<IApiResource> GetApiResources(DataTypeConfig dataType, string ticker, IComparablePeriod from, IComparablePeriod to)
        {
            if (from.CompareTo(to) > 0)
            {
                throw new ArgumentException($"from={from} is more than to={to}");
            }
            var endOfOndemandPeriod = taskHelper.FindEndOfOndemandPeriod(dataType, ticker, PeriodRange<IComparablePeriod>.Create(from, to), isOndemandEndpointEnabled);

            // use fixed tier from all range
            if (endOfOndemandPeriod is null)
            {
                var json = processor.Process(client.GetRange(dataType, ticker, from, to, false, true, true));
                return parser.ParseRange(dataType, json);
            }
            else
            {
                var ondemandTierJson = processor.Process(client.GetRange(dataType, ticker, from, endOfOndemandPeriod, true, true, true));
                var fixedTierJson = processor.Process(client.GetRange(dataType, ticker, endOfOndemandPeriod.Next(), to, false, true, true));

                var ondemandResources = parser.ParseRange(dataType, ondemandTierJson);
                var fixedTierResource = parser.ParseRange(dataType, fixedTierJson);

                return ondemandResources.Concat(fixedTierResource).ToList();
            }
        }
    }
}