using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BuffettCodeAddin
{
    public class Quarter : IPropertyAggregation
    {
        public string Ticker { get { return properties["ticker"]; } }

        public int FiscalYear { get { return Int32.Parse(properties["fiscal_year"]); } }

        public int FiscalQuarter { get { return Int32.Parse(properties["fiscal_quarter"]); } }

        private IDictionary<string, string> properties { get; set; }

        private IDictionary<string, PropertyDescrption> descriptions { get; set; }

        public Quarter(IDictionary<string, string> properties, IDictionary<string, PropertyDescrption> descriptions)
        {
            this.properties = properties;
            this.descriptions = descriptions;
        }

        public string GetValue(string propertyName)
        {
            if (!properties.Keys.Contains(propertyName))
            {
                throw new PropertyNotFoundException();
            }
            return properties[propertyName];
        }

        public PropertyDescrption GetDescription(string propertyName)
        {
            return descriptions.Keys.Contains(propertyName) ? descriptions[propertyName] : null;
        }

        public string GetQuarter()
        {
            return FiscalYear + "Q" + FiscalQuarter;
        }

        public IList<string> GetNames()
        {
            return properties.Keys.ToList();
        }

        public string GetIdentifier()
        {
            return Quarter.GetIdentifier(Ticker, FiscalYear, FiscalQuarter);
        }

        public static string GetIdentifier(string ticker, int fiscalYear, int fiscalQuarter)
        {
            return String.Join("_", ticker, fiscalYear, fiscalQuarter);
        }

        public static IList<Quarter> parse(string ticker, string jsonString)
        {
            JObject json = JsonConvert.DeserializeObject(jsonString) as JObject;

            IDictionary<string, PropertyDescrption> descriptions = null;
            IList<JToken> columnDescription = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => t.Path.Equals("column_description")).ToList();
            if (columnDescription.Count > 0)
            {
                descriptions = columnDescription.First().SelectMany(t => t.Children()).Where(t => t is JProperty).Cast<JProperty>().Select(t => ToPropertyDescription(t)).ToDictionary(p => p.Name, p => p);

            }

            IList<JToken> quarterData = json.Children().Where(t => t is JToken).Cast<JToken>().Where(t => !t.Path.Equals("column_description")).ToList();
            return quarterData.SelectMany(t => t.Children()).SelectMany(t => t.Children()).Select(t => ToQuarter(ticker, t, descriptions)).ToList();
        }

        private static Quarter ToQuarter(string ticker, JToken token, IDictionary<string, PropertyDescrption> descriptions)
        {
            IDictionary<string, string> properties = token.Where(t => t is JProperty).Cast<JProperty>().ToDictionary(p => p.Name, p => NormalizeValue(p.Value.ToString()));
            properties.Add("ticker", ticker);
            return new Quarter(properties, descriptions ?? ImmutableDictionary<string, PropertyDescrption>.Empty);
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
