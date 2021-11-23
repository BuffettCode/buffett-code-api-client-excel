using BuffettCodeAPIClient;
using BuffettCodeCommon;
using BuffettCodeCommon.Config;
using BuffettCodeCommon.Period;
using BuffettCodeIO.Parser;
using System.Collections.Generic;
using System.Linq;

namespace BuffettCodeIO.Resolver
{
    class ApiV3LegacyDataTypeResolver : ILegacyDataTypeResolver
    {
        private static readonly Dictionary<string, DataTypeConfig> mappingTable;
        private static readonly ApiV3LegacyDataTypeResolver instance = new ApiV3LegacyDataTypeResolver();

        private ApiV3LegacyDataTypeResolver()
        {
            var apiClient = ApiClientFactory.Create(BuffettCodeApiVersion.Version3, Configuration.GetInstance().ApiKey);

            var parser = ApiResponseParserFactory.Create(BuffettCodeApiVersion.Version3);

            // 適当にAPI をたたいて description を取得する
            var quarter = parser.Parse(
                DataTypeConfig.Quarter,
                apiClient.Get(DataTypeConfig.Quarter, ApiRequestParamConfig.ValueRepresentativeJpTicker, LatestFiscalQuarterPeriod.GetInstance(), false, false, true)
                );
            var daily = parser.Parse(
                DataTypeConfig.Daily,
                apiClient.Get(DataTypeConfig.Daily, ApiRequestParamConfig.ValueRepresentativeJpTicker, LatestDayPeriod.GetInstance(), false, false, true)
                );

            // quarter を優先で map を作る
            var propertyDataTypeDict = new Dictionary<string, DataTypeConfig>();

            quarter.GetPropertyNames().Select(name =>
               propertyDataTypeDict[name] = DataTypeConfig.Quarter
            );
            daily.GetPropertyNames().Where(name => propertyDataTypeDict.ContainsKey(name)).Select(name => propertyDataTypeDict[name] = DataTypeConfig.Daily);
        }

        public static ApiV3LegacyDataTypeResolver GetInstance()
        {
            return instance;
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