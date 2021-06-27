using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Parser
{
    public class ApiV2ResponseParser : IApiResponseParser
    {
        private static IList<JToken> FindColumnDescriptions(JObject json) => json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(PropertyNames.ColumnDescription)).ToList();

        private static IEnumerable<JToken> FindDataBody(JObject json) => json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => !t.Path.Equals(PropertyNames.ColumnDescription));

        public static Quarter ParseQuarter(JObject json)
        {
            IList<JToken> columnDescriptions = FindColumnDescriptions(json);
            IList<JToken> data = FindDataBody(json).ToList();

            var ticker = data.First().Path;
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions);
            var properties = ParseProperties(data.Children().Children().First());

            return CreateQuarter(ticker, properties, descriptions);
        }

        public static IList<Quarter> ParseQuarterRange(JObject json)
        {
            IList<JToken> columnDescriptions = FindColumnDescriptions(json);
            JToken data = FindDataBody(json).First();
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions);

            var ticker = data.Path;
            var list = data.Children().Children().ToList();
            return list.Select(d => CreateQuarter(ticker, ParseProperties(d), descriptions)).ToList();
        }

        public static Indicator ParseIndicator(JObject json)
        {
            IList<JToken> columnDescriptions = FindColumnDescriptions(json);
            IList<JToken> data = FindDataBody(json).ToList();

            var ticker = data.First().Path;
            var properteis = ParseProperties(data.Children().Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions);

            return Indicator.Create(
                ticker,
                properteis,
                descriptions
            );
        }

        public static Company ParseCompany(JObject json)
        {
            IList<JToken> columnDescriptions = FindColumnDescriptions(json);
            IList<JToken> data = FindDataBody(json).ToList();

            var ticker = data.First().Path;
            var properteis = ParseProperties(data.Children().Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions);

            return CreateCompany(
                ticker,
                properteis,
                descriptions
            );
        }
        private static PropertyDictionary ParseProperties(JToken data)
        {
            var jProperties = data.Children()
            .Where(t => t is JProperty).Cast<JProperty>();
            return PropertiesParser.Parse(jProperties);
        }

        private static Quarter CreateQuarter(string ticker,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            var fiscalYear = Convert.ToUInt16(properties.Get(PropertyNames.FiscalYear));
            var fiscalQuarter = Convert.ToUInt16(properties.Get(PropertyNames.FiscalQuarter));
            return Quarter.Create(
                ticker,
                fiscalYear,
                fiscalQuarter,
                properties,
                descriptions
            );
        }

        private static Company CreateCompany(string ticker,
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            var fixedTierRangeJson = JObject.Parse(properties.Get(PropertyNames.FixedTierRange));
            var fixedTierRange = FixedTierRangeParser.Parse(fixedTierRangeJson.Properties());
            var oldestFy = uint.Parse(properties.Get(PropertyNames.OldestFiscalYear));
            var oldestFq = uint.Parse(properties.Get(PropertyNames.OldestFiscalQuarter));

            var latestFy = uint.Parse(properties.Get(PropertyNames.LatestFiscalYear));
            var latestFq = uint.Parse(properties.Get(PropertyNames.LatestFiscalQuarter));

            var ondemandPeriodRange = PeriodRange<FiscalQuarterPeriod>.Create(
                FiscalQuarterPeriod.Create(oldestFy, oldestFq),
                FiscalQuarterPeriod.Create(latestFy, latestFq)
              );

            return Company.Create(
                ticker,
                fixedTierRange,
                ondemandPeriodRange,
                properties,
                descriptions
            );
        }



        public IApiResource Parse(DataTypeConfig dataType, JObject json)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return ParseQuarter(json);
                case DataTypeConfig.Indicator:
                    return ParseIndicator(json);
                default:
                    throw new NotSupportedDataTypeException($"Parse {dataType} is not supported at V2");
            }
        }

        public IList<IApiResource> ParseRange(DataTypeConfig dataType, JObject json)
        {
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return ParseQuarterRange(json).Cast<IApiResource>().ToList();
                default:
                    throw new NotSupportedDataTypeException($"ParseRange {dataType} is not supported at V3");
            }
        }
    }
}