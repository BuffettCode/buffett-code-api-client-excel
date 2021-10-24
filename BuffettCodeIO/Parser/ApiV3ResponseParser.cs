using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Exception;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Property;
using Newtonsoft.Json.Linq;


namespace BuffettCodeIO.Parser
{
    public class ApiV3ResponseParser : IApiResponseParser
    {
        private static IEnumerable<JToken> FindColumnDescriptions(JObject json) => json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(PropertyNames.ColumnDescription)).ToList();

        private static IEnumerable<JToken> FindDataBody(JObject json) => json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals(PropertyNames.Data));

        private static Quarter ParseQuarter(IEnumerable<JToken> columnDescriptions, IEnumerable<JToken> data)
        {
            var ticker = data.First().Path;
            var descriptions = ColumnDescriptionParser.Parse(columnDescriptions.ToList());
            var properties = ParseProperties(data.Children().Children().First());

            return CreateQuarter(ticker, properties, descriptions);
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
