using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BuffettCodeAddin
{
    public class Indicator : IPropertyAggregation
    {
        public string Ticker { get { return properties["ticker"]; } }

        private IDictionary<string, string> properties { get; set; }

        private IDictionary<string, PropertyDescrption> descriptions { get; set; }

        public Indicator(IDictionary<string, string> properties, IDictionary<string, PropertyDescrption> descriptions)
        {
            this.properties = properties;
            this.descriptions = descriptions;
        }

        public string GetValue(string name)
        {
            if (!properties.Keys.Contains(name))
            {
                throw new PropertyNotFoundException();
            }
            return properties[name];
        }

        public PropertyDescrption GetDescription(string name)
        {
            return properties.Keys.Contains(name) ? descriptions[name] : null;
        }

        public string GetIdentifier()
        {
            return Ticker;
        }

        public static IList<Indicator> parse(string ticker, string jsonString)
        {
            JObject json = JsonConvert.DeserializeObject(jsonString) as JObject;

            IDictionary<string, PropertyDescrption> descriptions = null;
            IList<JToken> columnDescription = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals("column_description")).ToList();
            if (columnDescription.Count > 0)
            {
                descriptions = columnDescription.First().SelectMany(t => t.Children()).Where(t => t is JProperty).Cast<JProperty>().Select(t => ToPropertyDescription(t)).ToDictionary(p => p.Name, p => p);

            }

            IList<JToken> indicatorData = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => !t.Path.Equals("column_description")).ToList();
            return indicatorData.SelectMany(t => t.Children()).SelectMany(t => t.Children()).Select(t => ToIndicator(ticker, t, descriptions)).ToList();
        }

        private static Indicator ToIndicator(string ticker, JToken token, IDictionary<string, PropertyDescrption> descriptions)
        {
            IDictionary<string, string> properties = token.Where(t => t is JProperty).Cast<JProperty>().ToDictionary(p => p.Name, p => NormalizeValue(p.Value.ToString()));
            properties.Add("ticker", ticker);
            return new Indicator(properties, descriptions ?? ImmutableDictionary<string, PropertyDescrption>.Empty);
        }

        private static PropertyDescrption ToPropertyDescription(JProperty property)
        {
            string name = property.Name;
            string label = property.Value["name_jp"].ToString();
            string unit = property.Value["unit"].ToString();
            return new PropertyDescrption(name, label, unit);
        }

        private static string NormalizeValue(string value)
        {
            if ("None".Equals(value))
            {
                return "";
            }
            return value;
        }

    }
}
