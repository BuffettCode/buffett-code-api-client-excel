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
        private static IEnumerable<JToken> FindColumnDescriptions(JObject json) => json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(PropertyNames.ColumnDescription)).ToList();

        private static IEnumerable<JToken> FindDataBody(JObject json) => json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => !t.Path.Equals(PropertyNames.ColumnDescription));

        private static Quarter ParseQuarter(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var ticker = data.First().Path;
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());
            var properties = ParseProperties(data.Children().Children().First());

            return CreateQuarter(ticker, properties, descriptions);
        }

        private static IList<Quarter> ParseQuarterRange(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());

            var ticker = data.First().Path;
            var list = data.Children().Children().ToList();
            return list.Select(d => CreateQuarter(ticker, ParseProperties(d), descriptions)).ToList();
        }

        private static Indicator ParseIndicator(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var ticker = data.First().Path;
            var properties = ParseProperties(data.Children().Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());

            return Indicator.Create(
                ticker,
                properties,
                descriptions
            );
        }

        private static Company ParseCompany(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var ticker = data.First().Path;
            var properties = ParseProperties(data.Children().Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());

            return CreateCompany(
                ticker,
                properties,
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
            var oldestDate = DayPeriod.Parse(properties.Get(PropertyNames.OldestDate));
            // use today as latest date
            var latestDate = DayPeriod.Create(DateTime.Today);

            var fixedTierQuarterRange = PeriodRange<FiscalQuarterPeriod>.Create(fixedTierRange.OldestQuarter, fixedTierRange.LatestQuarter);

            var fixedTierDayRange = PeriodRange<DayPeriod>.Create(fixedTierRange.OldestDate, fixedTierRange.LatestDate);

            var ondemandTierQuarterRange = PeriodRange<FiscalQuarterPeriod>.Create(
                FiscalQuarterPeriod.Create(oldestFy, oldestFq),
                FiscalQuarterPeriod.Create(latestFy, latestFq)
              );
            var ondemandTierDayRange = PeriodRange<DayPeriod>.Create(oldestDate, latestDate);

            return Company.Create(
                ticker,
                fixedTierQuarterRange,
                ondemandTierQuarterRange,
                fixedTierDayRange,
                ondemandTierDayRange,
                properties,
                descriptions
            );
        }

        public IApiResource Parse(DataTypeConfig dataType, JObject json)
        {
            var columnDescriptions = FindColumnDescriptions(json);
            var data = FindDataBody(json);
            if (data.Count() == 0)
            {
                return EmptyResource.GetInstance();
            }
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return ParseQuarter(columnDescriptions, data);
                case DataTypeConfig.Indicator:
                    return ParseIndicator(columnDescriptions, data);
                case DataTypeConfig.Company:
                    return ParseCompany(columnDescriptions, data);
                default:
                    throw new NotSupportedDataTypeException($"Parse {dataType} is not supported at V2");
            }
        }

        public IList<IApiResource> ParseRange(DataTypeConfig dataType, JObject json)
        {
            var columnDescriptions = FindColumnDescriptions(json);
            var data = FindDataBody(json);
            if (data.Count() == 0)
            {
                // return Empty 
                return new List<IApiResource>().ToList();
            }
            switch (dataType)
            {
                case DataTypeConfig.Quarter:
                    return ParseQuarterRange(columnDescriptions, data).Cast<IApiResource>().ToList();
                default:
                    throw new NotSupportedDataTypeException($"ParseRange {dataType} is not supported at V2");
            }
        }
    }
}