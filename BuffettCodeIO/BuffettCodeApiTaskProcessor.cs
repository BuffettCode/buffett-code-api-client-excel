using BuffettCodeAPIClient;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Parser;
using BuffettCodeIO.Processor;
using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BuffettCodeIO
{
    public class BuffettCodeApiTaskProcessor
    {
        private readonly IBuffettCodeApiClient client;
        private readonly IApiResponseParser parser;
        private readonly ITaskProcessor<JObject> processor;
        private static readonly object updateLock = new object();

        public BuffettCodeApiTaskProcessor(BuffettCodeApiVersion version, string apiKey, uint maxDegreeOfParallelism)
        {
            client = ApiClientInstanceGetter.Get(version, apiKey);
            parser = ApiResponseParserFactory.Create(version);
            processor = new SemaphoreTaskProcessor<JObject>(maxDegreeOfParallelism);
        }

        public BuffettCodeApiTaskProcessor UpdateIfNeeded(string apiKey, uint maxDegreeOfParallelism)
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
            }
            return this;
        }

        public IApiResource GetApiResource(DataTypeConfig dataType, string ticker, IPeriod period)
        {
            var json = processor.Process(client.Get(dataType, ticker, period, false, true, true));
            return parser.Parse(dataType, json);
        }

        public IList<IApiResource> GetApiResources(DataTypeConfig dataType, string ticker, IPeriod from, IPeriod to)
        {
            var json = processor.Process(client.GetRange(dataType, ticker, from, to, false, true, true));
            return parser.ParseRange(dataType, json);
        }
    }
}