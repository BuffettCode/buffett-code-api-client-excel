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

        private static PropertyDictionary ParseProperties(JToken propertiesRoot)
        {
            var root = propertiesRoot.ToObject<JObject>();
            var jProperties = root.Children()
            .Where(t => t is JProperty).Cast<JProperty>();
            return PropertiesParser.Parse(jProperties);
        }

        private static Company ParseCompany(
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

            var ondemandPeriodRange = PeriodRange<FiscalQuarterPeriod>.Create(
                FiscalQuarterPeriod.Create(oldestFy, oldestFq),
                FiscalQuarterPeriod.Create(latestFy, latestFq)
              );
            var ondemandTierDayRange = PeriodRange<DayPeriod>.Create(oldestDate, latestDate);

            return Company.Create(
                properties.Get(PropertyNames.Ticker),
                fixedTierQuarterRange,
                ondemandPeriodRange,
                fixedTierDayRange,
                ondemandTierDayRange,
                properties,
                descriptions
            );
        }

        private static Quarter ParseQuarter(
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

        private static Daily ParseDaily(PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            var period = DayPeriod.Parse(properties.Get(PropertyNames.Day));
            return Daily.Create(
                properties.Get(PropertyNames.Ticker),
                period,
                properties,
                descriptions
            );
        }

        private static Monthly ParseMonthly(PropertyDictionary properties, PropertyDescriptionDictionary descriptions)
        {
            var year = Convert.ToUInt16(properties.Get(PropertyNames.Year));
            var month = Convert.ToUInt16(properties.Get(PropertyNames.Month));
            return Monthly.Create(properties.Get(PropertyNames.Ticker),
                year,
                month,
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
            else
            {
                var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());
                var properties = ParseProperties(data.Children().First());
                switch (dataType)
                {
                    case DataTypeConfig.Company:
                        return ParseCompany(properties, descriptions);
                    case DataTypeConfig.Quarter:
                        return ParseQuarter(properties, descriptions);
                    case DataTypeConfig.Monthly:
                        return ParseMonthly(properties, descriptions);
                    case DataTypeConfig.Daily:
                        return ParseDaily(properties, descriptions);
                    default:
                        throw new NotSupportedDataTypeException($"Parse {dataType} is not supported at V3");
                }
            }

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
            else
            {
                var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());
                switch (dataType)
                {
                    case DataTypeConfig.Quarter:
                        return data.Children().Children().Children().Select(d => ParseQuarter(ParseProperties(d), descriptions)).Cast<IApiResource>().ToList();
                    default:
                        throw new NotSupportedDataTypeException($"Parse {dataType} is not supported at V3");
                }
            }
        }
    }
}