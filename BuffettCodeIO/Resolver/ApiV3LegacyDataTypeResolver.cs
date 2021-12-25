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
        private readonly Dictionary<string, DataTypeConfig> mappingTable = new Dictionary<string, DataTypeConfig>();
        private static readonly ApiV3LegacyDataTypeResolver instance = new ApiV3LegacyDataTypeResolver();
        private static readonly TickerQuarterParameter quarterParameter = TickerQuarterParameter.Create(ApiRequestParamConfig.ValueRepresentativeJpTicker, RelativeFiscalQuarterPeriod.CreateLatest());
        private static readonly TickerDayParameter dayParameter = TickerDayParameter.Create(ApiRequestParamConfig.ValueRepresentativeJpTicker, LatestDayPeriod.GetInstance());

        private ApiV3LegacyDataTypeResolver()
        {
            var apiClient = ApiClientFactory.Create(BuffettCodeApiVersion.Version3, Configuration.GetInstance().ApiKey);

            var parser = ApiResponseParserFactory.Create(BuffettCodeApiVersion.Version3);

            // 適当にAPI をたたいて description を取得する
            var quarter = parser.Parse(
                DataTypeConfig.Quarter,
                apiClient.Get(DataTypeConfig.Quarter, quarterParameter, false, false, true)
                );
            var daily = parser.Parse(
                DataTypeConfig.Daily,
                apiClient.Get(DataTypeConfig.Daily, dayParameter, false, false, true)
                );

            // quarter を優先で map を作る
            foreach (string name in quarter.GetPropertyNames())
            {
                mappingTable[name] = DataTypeConfig.Quarter;

            }

            foreach (string name in daily.GetPropertyNames().Where(name => !mappingTable.ContainsKey(name)))
            {
                mappingTable[name] = DataTypeConfig.Daily;
            }
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