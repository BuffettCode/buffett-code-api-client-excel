using BuffettCodeIO.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO
{
    /// <summary>
    /// 財務数値
    /// </summary>
    /// <remarks>
    /// /api/{version}/quarter のレスポンスに対応します。
    /// </remarks>
    public class Quarter : IPropertyAggregation
    {
        /// <summary>
        /// 銘柄コード
        /// </summary>
        public string Ticker { get { return properties["ticker"]; } }

        /// <summary>
        /// 会計年度
        /// </summary>
        public int FiscalYear { get { return Int32.Parse(properties["fiscal_year"]); } }

        /// <summary>
        /// 会計四半期
        /// </summary>
        public int FiscalQuarter { get { return Int32.Parse(properties["fiscal_quarter"]); } }

        private IDictionary<string, string> properties { get; set; }

        private IDictionary<string, PropertyDescrption> descriptions { get; set; }

        public Quarter(IDictionary<string, string> properties, IDictionary<string, PropertyDescrption> descriptions)
        {
            this.properties = properties;
            this.descriptions = descriptions;
        }

        /// <inheritdoc/>
        public string GetValue(string propertyName)
        {
            if (!properties.Keys.Contains(propertyName))
            {
                throw new PropertyNotFoundException();
            }
            return properties[propertyName];
        }

        /// <inheritdoc/>
        public PropertyDescrption GetDescription(string propertyName)
        {
            return descriptions.Keys.Contains(propertyName) ? descriptions[propertyName] : null;
        }

        /// <summary>
        /// 項目名のリストを取得します。
        /// </summary>
        /// <returns>項目名のリスト</returns>
        public IList<string> GetNames()
        {
            return properties.Keys.ToList();
        }

        /// <inheritdoc/>
        public string GetIdentifier()
        {
            return Quarter.GetIdentifier(Ticker, FiscalYear, FiscalQuarter);
        }

        public static string GetIdentifier(string ticker, int fiscalYear, int fiscalQuarter)
        {
            return String.Join("_", ticker, fiscalYear, fiscalQuarter);
        }

        /// <summary>
        /// バフェットコードWebAPIのレスポンスをパースします。
        /// </summary>
        /// <param name="ticker">銘柄コード</param>
        /// <param name="jsonString">JSON文字列</param>
        /// <returns><see cref="Quarter"/>のリスト</returns>
        public static IList<Quarter> Parse(string ticker, string jsonString)
        {
            JObject json = JsonConvert.DeserializeObject(jsonString) as JObject;
            APIResponseValidator.Validate(json);

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
            return new Quarter(properties, descriptions ?? new Dictionary<string, PropertyDescrption>());
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
