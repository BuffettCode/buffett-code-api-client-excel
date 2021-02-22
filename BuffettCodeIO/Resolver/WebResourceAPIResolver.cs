using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BuffettCodeIO.Resolver
{
    /// <summary>
    /// Webから取得した定義ファイルを用いるAPIリゾルバ
    /// </summary>
    public class WebResourceAPIResolver : IAPIResolver
    {
        private static readonly Dictionary<string, APIType> mappingTable;

        private static readonly Dictionary<APIType, string> propertyDescriptionDefinitions = new Dictionary<APIType, string>
        {
            {APIType.Quarter, @"https://docs.buffett-code.com/v2-quarter.json" },
            {APIType.Indicator, @"https://docs.buffett-code.com/v2-indicator.json" }
        };

        private static WebResourceAPIResolver _instance = new WebResourceAPIResolver();
        public static WebResourceAPIResolver GetInstance()
        {
            return _instance;
        }

        static WebResourceAPIResolver()
        {
            mappingTable = CreateMappingTable();
        }

        private static Dictionary<string, APIType> CreateMappingTable()
        {
            var result = new Dictionary<string, APIType>();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                foreach (var api in propertyDescriptionDefinitions.Keys)
                {
                    var url = propertyDescriptionDefinitions[api];
                    var req = WebRequest.Create(url);
                    var reader = new StreamReader(req.GetResponse().GetResponseStream());
                    var descriptions = Parse(reader.ReadToEnd());
                    foreach (var description in descriptions)
                    {
                        result[description.Name] = api;
                    }
                }
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                return null;
            }

            return result;
        }

        private static IList<PropertyDescrption> Parse(string jsonString)
        {
            JObject json = JsonConvert.DeserializeObject(jsonString) as JObject;
            return json.Children().Where(t => t is JProperty).Cast<JProperty>().Select(t => ToPropertyDescription(t)).ToList();
        }

        private static PropertyDescrption ToPropertyDescription(JProperty property)
        {
            string name = property.Name;
            string label = property.Value["name_jp"].ToString();
            string unit = property.Value["unit"].ToString();
            return new PropertyDescrption(name, label, unit);
        }
        /// <summary>
        /// 初期化に成功したかどうかを返します。
        /// </summary>
        /// <remarks>
        /// Webに配置されたAPIごとの項目定義JSONを読み込み、パースに成功した場合にtrueを返します。
        /// このメソッドがtrueを返したらWebResourceAPIResolverを正常に利用できます。
        /// </remarks>
        /// <returns>初期化に成功したか</returns>
        public static bool IsInitialized()
        {
            return mappingTable != null && mappingTable.Count > 0;
        }

        /// <inheritdoc/>
        public APIType Resolve(string propertyName)
        {
            if (mappingTable.ContainsKey(propertyName))
            {
                return mappingTable[propertyName];
            } else
            {
                return APIType.Quarter; // default value
            }
        }
    }
}
