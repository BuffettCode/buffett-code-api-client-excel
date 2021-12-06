using BuffettCodeCommon.Config;
using BuffettCodeIO.Property;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BuffettCodeIO.Resolver
{

    public class ApiV2LegacyDataTypeResolver : ILegacyDataTypeResolver
    {
        private readonly Dictionary<string, DataTypeConfig> mappingTable;

        private readonly Dictionary<DataTypeConfig, string> propertyDescriptionDefinitions = new Dictionary<DataTypeConfig, string>
        {
            {DataTypeConfig.Quarter, @"https://docs.buffett-code.com/v2-quarter.json" },
            {DataTypeConfig.Indicator, @"https://docs.buffett-code.com/v2-indicator.json" }
        };

        private static readonly ApiV2LegacyDataTypeResolver instance = new ApiV2LegacyDataTypeResolver();

        public static ApiV2LegacyDataTypeResolver GetInstance()
        {
            return instance;
        }

        private ApiV2LegacyDataTypeResolver()
        {
            var supportedDataTypes = new DataTypeConfig[] { DataTypeConfig.Quarter, DataTypeConfig.Indicator };
            mappingTable = new Dictionary<string, DataTypeConfig>();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            foreach (var dataType in supportedDataTypes)
            {
                var url = propertyDescriptionDefinitions[dataType];
                var req = WebRequest.Create(url);
                var reader = new StreamReader(req.GetResponse().GetResponseStream());
                var descriptions = Parse(reader.ReadToEnd());
                foreach (var description in descriptions)
                {
                    // Quarter を優先してmapping table を作る
                    if (!mappingTable.ContainsKey(description.Name))
                    {
                        mappingTable[description.Name] = dataType;
                    }
                }
            }
        }

        private static IList<PropertyDescription> Parse(string jsonString)
        {
            JObject json = JsonConvert.DeserializeObject(jsonString) as JObject;
            return json.Children().Where(t => t is JProperty).Cast<JProperty>().Select(t => ToPropertyDescription(t)).ToList();
        }

        private static PropertyDescription ToPropertyDescription(JProperty property)
        {
            string name = property.Name;
            string label = property.Value["name_jp"].ToString();
            string unit = property.Value["unit"].ToString();
            return new PropertyDescription(name, label, unit);
        }

        public DataTypeConfig Resolve(string propertyName)
        {
            if (mappingTable.ContainsKey(propertyName))
            {
                return mappingTable[propertyName];
            }
            else
            {
                return DataTypeConfig.Quarter; // default value
            }
        }
    }
}