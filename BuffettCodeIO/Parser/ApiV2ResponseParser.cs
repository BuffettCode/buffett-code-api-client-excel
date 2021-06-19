using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Parser
{
    public class ApiV2ResponseParser : IApiResponseParser
    {
        public static Quarter ParseQuarter(JObject json)
        {
            IList<JToken> columnDescription = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(PropertyNames.ColumnDescription)).ToList();
            IList<JToken> data = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => !t.Path.Equals(PropertyNames.ColumnDescription)).ToList();

            var ticker = data.First().Path;
            var descriptions = ColumnDescriptionParser.Parse(columnDescription);
            var properties = ParseProperties(data.Children().Children().First());

            return CreateQuarter(ticker, properties, descriptions);
        }

        public static IList<Quarter> ParseQuarterRange(JObject json)
        {
            IList<JToken> columnDescription = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(PropertyNames.ColumnDescription)).ToList();
            JToken data = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => !t.Path.Equals(PropertyNames.ColumnDescription)).First();
            var descriptions = ColumnDescriptionParser.Parse(columnDescription);

            var ticker = data.Path;
            var list = data.Children().Children().ToList();
            return list.Select(d => CreateQuarter(ticker, ParseProperties(d), descriptions)).ToList();
        }

        public static Indicator ParseIndicator(JObject json)
        {
            IList<JToken> columnDescription = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(PropertyNames.ColumnDescription)).ToList();
            IList<JToken> data = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => !t.Path.Equals(PropertyNames.ColumnDescription)).ToList();

            var ticker = data.First().Path;
            var properteis = ParseProperties(data.Children().Children().First());
            var descriptions = ColumnDescriptionParser.Parse(columnDescription);

            return Indicator.Create(
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

    }
}