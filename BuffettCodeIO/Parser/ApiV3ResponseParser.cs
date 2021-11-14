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
    public class ApiV3ResponseParser : IApiResponseParser
    {
        private static IEnumerable<JToken> FindProperty(string root, JObject json) => json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(root)).ToList();

        private static PropertyDictionary ParseProperties(JToken data)
        {
            var jProperties = data.Children()
            .Where(t => t is JProperty).Cast<JProperty>();
            return PropertiesParser.Parse(jProperties);
        }

        private static Company ParseCompany(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var properties = ParseProperties(data.Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());

            return CreateCompany(
                properties,
                descriptions
            );
        }

        private static Company CreateCompany(
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
                properties.Get(PropertyNames.Ticker),
                fixedTierRange,
                ondemandPeriodRange,
                properties,
                descriptions
            );
        }

        private static Quarter ParseQuarter(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var properties = ParseProperties(data.Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());
            return CreateQuarter(properties, descriptions);
        }

        private static Quarter CreateQuarter(
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            var fiscalYear = Convert.ToUInt16(properties.Get(PropertyNames.FiscalYear));
            var fiscalQuarter = Convert.ToUInt16(properties.Get(PropertyNames.FiscalQuarter));
            return Quarter.Create(
                properties.Get(PropertyNames.Ticker),
                fiscalYear,
                fiscalQuarter,
                properties,
                descriptions
            );
        }

        private static Daily ParseDaily(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var properties = ParseProperties(data.Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());
            return CreateDaily(properties, descriptions);
        }

        private static Daily CreateDaily(
            PropertyDictionary properties,
            PropertyDescriptionDictionary descriptions)
        {
            var period = DayPeriod.Parse(properties.Get(PropertyNames.Day));
            return Daily.Create(
                properties.Get(PropertyNames.Ticker),
                period,
                properties,
                descriptions
            );
        }

        public IApiResource Parse(DataTypeConfig dataType, JObject json)
        {
            var columnDescriptions = FindProperty(PropertyNames.ColumnDescription, json);
            var data = FindProperty(PropertyNames.Data, json);
            if (data.Count() == 0)
            {
                return EmptyResource.GetInstance();
            }
            switch (dataType)
            {
                case DataTypeConfig.Company:
                    return ParseCompany(columnDescriptions, data);
                case DataTypeConfig.Quarter:
                    return ParseQuarter(columnDescriptions, data);
                case DataTypeConfig.Daily:
                    return ParseDaily(columnDescriptions, data);
                default:
                    throw new NotSupportedDataTypeException($"Parse {dataType} is not supported at V3");
            }
        }

        private static IList<Quarter> ParseBulkQuarter(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());

            return data.Children().Children().First().Select(d => CreateQuarter(ParseProperties(d), descriptions)).ToList();
        }

        public IList<IApiResource> ParseRange(DataTypeConfig dataType, JObject json)
        {
            var columnDescriptions = FindProperty(PropertyNames.ColumnDescription, json);
            var data = FindProperty(PropertyNames.Data, json);
            if (data.Count() == 0)
            {
                // return Empty 
                return new List<IApiResource>().ToList();
            }
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return ParseBulkQuarter(columnDescriptions, data).Cast<IApiResource>().ToList();
                default:
                    throw new NotSupportedDataTypeException($"Parse {dataType} is not supported at V3");
            }
        }


    }
}